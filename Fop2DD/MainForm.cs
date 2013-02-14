using Fop2ClientLib;
using GlobalHotKey;
using System;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections.Generic;
using si = System.Windows.Input;

namespace Fop2DD
{
    public partial class MainForm : Form
    {
        private Fop2FatClient _client;

        private HotKeyManager _hotkeymanager;
        private PhonenumberGrabber _phonenumbergrabber;

        private bool _autoreconnect;
        private bool _isexiting;
        private HotKey _dialhotkey;
        private bool _authfailed;

        public MainForm()
        {
            InitializeComponent();

            _autoreconnect = false;
            _isexiting = false;

            _client = new Fop2FatClient();

            _hotkeymanager = new HotKeyManager();
            _hotkeymanager.KeyPressed += hotkeymanager_KeyPressed;

            _phonenumbergrabber = new PhonenumberGrabber();

            hotkeyComboBox.DataSource = Enum.GetValues(typeof(si.Key)).Cast<si.Key>();

            this.SetTrayStatus(null);

            var settings = Properties.Settings.Default;
            this.hostTextBox.Text = settings.Host;
            this.portTextBox.Text = settings.Port.ToString();
            this.usernameTextBox.Text = settings.Username;
            this.passwordTextBox.Text = settings.Password;
            this.contextTextBox.Text = settings.PBXContext;
            this.pingIntervalTextBox.Text = settings.PingInterval.ToString();

            hotkeyComboBox.SelectedItem = (si.Key)settings.Hotkey;
            hotkeyAltCheckBox.Checked = (settings.HotkeyModifiers & (int)si.ModifierKeys.Alt) != 0;
            hotkeyCtrlCheckBox.Checked = (settings.HotkeyModifiers & (int)si.ModifierKeys.Control) != 0;
            hotkeyWinCheckBox.Checked = (settings.HotkeyModifiers & (int)si.ModifierKeys.Windows) != 0;
            hotkeyShiftCheckBox.Checked = (settings.HotkeyModifiers & (int)si.ModifierKeys.Shift) != 0;
        }

        private void hotkeymanager_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var numbers = _phonenumbergrabber.TryGrabPhonenumbersFromSelection(6);  //TODO: Make this minlength a setting?
            if (numbers.Length == 0)
                return;                 //Nothing to do...

            string dialnumber = null;
            if (numbers.Length == 1)
            {
                dialnumber = numbers[0];
            }
            else
            {
                using (var s = new SelectNumber(numbers))
                {
                    if (s.ShowDialog(this) == DialogResult.OK)
                        dialnumber = s.SelectedNumber;
                }
            }

            if (dialnumber != null)
                _client.Dial(dialnumber);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _client.ConnectionError += _client_ConnectionError;
            _client.ConnectionStateChanged += _client_ConnectionStateChanged;
            _client.AuthenticationResultReceived += _client_AuthenticationResultReceived;
        }

        private void _client_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            if (_dialhotkey != null)
                _hotkeymanager.Unregister(_dialhotkey);
            this.SetFormState(false);
            this.SetTrayStatus("Connection error: " + e.Exception.Message, "error");
        }

        private void _client_AuthenticationResultReceived(object sender, AuthenticationResultReceivedEventArgs e)
        {
            if (e.Result == AuthenticationStatus.Success)
            {
                this.SetTrayStatus("Online", "online");
                _autoreconnect = true;

                Properties.Settings.Default.Save();
            }
            else
            {
                _authfailed = true;
                _client.Disconnect();
            }
        }

        private void _client_ConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            if (e.ConnectionState == ConnectionState.Connected)
            {
                this.SetTrayStatus("Connected", "online");
                _client.Authenticate(contextTextBox.Text, usernameTextBox.Text, passwordTextBox.Text);
            }
            else
            {
                if (_dialhotkey != null)
                    _hotkeymanager.Unregister(_dialhotkey);

                if (_authfailed)
                    this.SetTrayStatus("Logon failed", "authfailure");
                else
                    this.SetTrayStatus("Disconnected", "offline");

                if (_autoreconnect)
                    this.Connect();
            }
            this.SetFormState(e.ConnectionState == ConnectionState.Connected);
        }

        private void SetFormState(bool isconnected)
        {
            if (isconnected)
            {
                connectButton.Text = "Disconnect";
            }
            else
            {
                connectButton.Text = "Apply";
            }
            connectButton.Enabled = true;
            authenticationBox.Enabled = !isconnected;
            connectionBox.Enabled = !isconnected;
            hotkeyBox.Enabled = !isconnected;
        }

        private void Connect()
        {
            _autoreconnect = false;
            _authfailed = false;

            var settings = Properties.Settings.Default;
            settings.Host = this.hostTextBox.Text;
            settings.Port = int.Parse(this.portTextBox.Text);
            settings.PingInterval = int.Parse(pingIntervalTextBox.Text);
            settings.Username = this.usernameTextBox.Text;
            settings.Password = this.passwordTextBox.Text;
            settings.PBXContext = this.contextTextBox.Text;

            if (_dialhotkey != null)
                _hotkeymanager.Unregister(_dialhotkey);

            if ((si.Key)hotkeyComboBox.SelectedValue != si.Key.None)
            {
                si.ModifierKeys modifiers = si.ModifierKeys.None;
                modifiers |= hotkeyAltCheckBox.Checked ? si.ModifierKeys.Alt : si.ModifierKeys.None;
                modifiers |= hotkeyCtrlCheckBox.Checked ? si.ModifierKeys.Control : si.ModifierKeys.None;
                modifiers |= hotkeyWinCheckBox.Checked ? si.ModifierKeys.Windows : si.ModifierKeys.None;
                modifiers |= hotkeyShiftCheckBox.Checked ? si.ModifierKeys.Shift : si.ModifierKeys.None;

                _dialhotkey = new HotKey((si.Key)hotkeyComboBox.SelectedValue, modifiers);
                _hotkeymanager.Register(_dialhotkey);

                settings.Hotkey = (int)_dialhotkey.Key;
                settings.HotkeyModifiers = (int)_dialhotkey.Modifiers;
            }
            else
            {
                _dialhotkey = null;
            }

            var ipendpoint = string.Format("{0}:{1}", settings.Host, settings.Port);
            var pinginterval = TimeSpan.FromSeconds(settings.PingInterval);

            _client.HeartbeatInterval = pinginterval;
            _client.Connect(ipendpoint);

            this.SetTrayStatus(string.Format("Connecting to {0}", ipendpoint));
        }

        private void SetTrayStatus(string status)
        {
            this.SetTrayStatus(status, null);
        }

        private void SetTrayStatus(string status, string icon)
        {
            toolStripStatusLabel.Text = status;

            //Max length for notifyicon tooltip is 63 chars
            var tooltip = (status ?? string.Empty);
            NotifyIcon.Text = (tooltip.Length > 63) ? tooltip.Substring(0, 63) : tooltip; ;
            if (icon != null)
                NotifyIcon.Icon = Iconhandler.LoadIcon(icon);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            if (_client.IsConnected)
            {
                _autoreconnect = false;
                _client.Disconnect();
            }
            else
            {
                this.Connect();
            }
        }

        public void NotifyPopup(string title, string text)
        {
            this.NotifyPopup(title, text, TimeSpan.FromSeconds(30));
        }

        public void NotifyPopup(string title, string text, TimeSpan duration)
        {
            this.NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.NotifyIcon.BalloonTipTitle = title;
            this.NotifyIcon.BalloonTipText = text;
            this.NotifyIcon.ShowBalloonTip((int)duration.TotalMilliseconds);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isexiting && (e.CloseReason == CloseReason.UserClosing))
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                //Un"bind" the events so they won't cause trouble when being raised while we're busy closing and cleaning up.
                _client.ConnectionError -= _client_ConnectionError;
                _client.ConnectionStateChanged -= _client_ConnectionStateChanged;
                _client.AuthenticationResultReceived -= _client_AuthenticationResultReceived;

                _autoreconnect = false;
                _client.Disconnect();

                foreach (var f in this.OwnedForms)
                    f.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _hotkeymanager.Dispose();
            Application.Exit();
        }

        private void showAdvancedLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (advancedConnection.Visible)
            {
                showAdvancedLabel.Text = showAdvancedLabel.Text.Replace("▾", "▸");
            }
            else
            {
                showAdvancedLabel.Text = showAdvancedLabel.Text.Replace("▸", "▾");
            }
            advancedConnection.Visible = !advancedConnection.Visible;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Unhide();
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Trace.WriteLine("Balloon clicked!");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isexiting = true;
            this.Close();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Unhide();
        }

        private void Unhide()
        {
            this.Show();
            this.Activate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new AboutBox())
                f.ShowDialog(this);
        }
    }

    //http://stackoverflow.com/a/2904279
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    public class SpringLabel : ToolStripStatusLabel
    {
        public SpringLabel()
        {
            this.Spring = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            var flags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            var bounds = new Rectangle(0, 0, this.Bounds.Width, this.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, bounds, this.ForeColor, flags);
        }
    }
}
