using Fop2ClientLib;
using GlobalHotKey;
using System;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Forms.Design;

namespace Fop2DD
{
    public partial class MainForm : Form
    {
        private Fop2FatClient _client;

        private HotKeyManager _hotkeymanager;
        private PhonenumberGrabber _phonenumbergrabber;

        private bool _autoreconnect;
        private bool _isexiting;

        public MainForm()
        {
            InitializeComponent();

            _autoreconnect = false;
            _isexiting = false;

            _client = new Fop2FatClient();

            _hotkeymanager = new HotKeyManager();
            _hotkeymanager.KeyPressed += hotkeymanager_KeyPressed;
            
            //TODO: make this a setting
            _hotkeymanager.Register(Key.F8, System.Windows.Input.ModifierKeys.Control);

            _phonenumbergrabber = new PhonenumberGrabber();

            this.SetTrayStatus(null);
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
            this.SetFormState(false);
            this.SetTrayStatus("Connection error: " + e.Exception.Message, "error");
        }

        private void _client_AuthenticationResultReceived(object sender, AuthenticationResultReceivedEventArgs e)
        {
            if (e.Result == AuthenticationStatus.Success)
            {
                this.SetTrayStatus("Online", "online");
                _autoreconnect = true;
            }
            else
            {
                this.SetTrayStatus("Logon failed", "authfailure");
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
                connectButton.Text = "Connect";
            }
            connectButton.Enabled = true;
            authenticationBox.Enabled = !isconnected;
            connectionBox.Enabled = !isconnected;
        }

        private void Connect()
        {
            _autoreconnect = false;

            var ipendpoint = string.Format("{0}:{1}", this.hostTextBox.Text, this.portTextBox.Text);
            var pinginterval = TimeSpan.FromSeconds(int.Parse(pingIntervalTextBox.Text));

            if (_client.IsConnected)
                _client.Disconnect();

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
            if (_client.IsConnected)
            {
                _autoreconnect = false;
                _client.Disconnect();
            }
            else
            {
                this.Connect();
            }
            connectButton.Enabled = false;
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
