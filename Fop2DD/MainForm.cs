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
        private bool _isclosing;

        public MainForm()
        {
            InitializeComponent();

            _autoreconnect = false;
            _isclosing = false;

            _client = new Fop2FatClient();

            _hotkeymanager = new HotKeyManager();
            _hotkeymanager.KeyPressed += hotkeymanager_KeyPressed;
            _hotkeymanager.Register(Key.F8, System.Windows.Input.ModifierKeys.Control);

            _phonenumbergrabber = new PhonenumberGrabber();

            this.SetTrayStatus(null);
        }

        private void hotkeymanager_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var numbers = _phonenumbergrabber.TryGrabPhonenumbersFromSelection(6);
            //_client.Dial(numbers.First());
            Trace.WriteLine(string.Join("\r\n", numbers));
        }

        //private void _client_MessageSent(object sender, MessageSentEventArgs e)
        //{
        //    Trace.WriteLine("Sent: " + e.Message);
        //}

        //private void _client_MessageReceived(object sender, MessageReceivedEventArgs e)
        //{
        //    Trace.WriteLine(string.Format("Received:\r\n\tBtn : [{0}]\r\n\tCmd : [{1}]\r\n\tData: [{2}]\r\n\tSlot: [{3}]", e.Message.Button, e.Message.Command, e.Message.Data, e.Message.Slot));
        //}


        private void MainForm_Load(object sender, EventArgs e)
        {
            //_client.MessageReceived += _client_MessageReceived;
            //_client.MessageSent += _client_MessageSent;
            _client.ConnectionError += (s, ce) => { this.SetTrayStatus("Connection error: " + ce.Exception.Message, "error"); };
            _client.ConnectionStateChanged += (s, ce) =>
            {
                if (ce.ConnectionState == ConnectionState.Connected)
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
            };
            _client.AuthenticationResultReceived += (s, ae) => {
                if (ae.Result==AuthenticationStatus.Success) {
                    this.SetTrayStatus("Online", "online"); 
                    _autoreconnect = true;
                } else {
                    this.SetTrayStatus("Logon failed", "authfailure");
                }
            };
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
            if (!_isclosing)
            {
                toolStripStatusLabel.Text = status;

                //Max length for notifyicon tooltip is 63 chars
                var tooltip = (status ?? string.Empty);
                NotifyIcon.Text = (tooltip.Length > 63) ? tooltip.Substring(0, 63) : tooltip; ;
                if (icon != null)
                    NotifyIcon.Icon = Iconhandler.LoadIcon(icon);
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            this.Connect();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    _client.Disconnect();
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    _client.Authenticate("TESTA", "101", "1234");
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    _client.Dial("0492390112");
        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    this.NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
        //    this.NotifyIcon.BalloonTipText = "Lorem ipsum dolor sit amet ipsum dolor sit amet, consectetur adipiscing elit. Ut convallis, erat eu fringilla accumsan, enim ante porta libero, id vulputate enim enim ornare ante. Etiam in lorem nulla. Donec tincidunt interdum urna sit amet condimentum. Aliquam erat volutpat.";
        //    this.NotifyIcon.BalloonTipTitle = "Lorem ipsum dolor sit amet";
        //    this.NotifyIcon.Visible = true;
        //    this.NotifyIcon.ShowBalloonTip(30 * 1000);
        //    this.NotifyIcon.BalloonTipClicked += (s, ea) => { Trace.Write("Balloon clicked"); };
        //}

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isclosing = true;
            _autoreconnect = false;
            _client.Disconnect();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _hotkeymanager.Dispose();
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
            this.Show();
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
