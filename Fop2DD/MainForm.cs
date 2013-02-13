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

        public MainForm()
        {
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            _client.MessageReceived += _client_MessageReceived;
            _client.MessageSent += _client_MessageSent;
            _client.Connected += (s) => { this.SetTrayStatus("Connected", "online"); };
            _client.Disconnected += (s) => { this.SetTrayStatus("Disconnected", "offline"); };
            _client.ConnectionError += (s, ce) => { this.SetTrayStatus("Connection error: " + ce.Exception.Message, "error"); };
            _client.AuthenticationFailed += (s) => { this.SetTrayStatus("Logon failed", "authfailure"); };
            _client.AuthenticationSucceeded += (s) => { this.SetTrayStatus("Online", "online"); };

            //_client.Heartbeat += (s, hea) => { Trace.WriteLine("Heartbeat"); };

            textBox1.Text = _client.HeartbeatInterval.Seconds.ToString();
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

        void _client_MessageSent(object sender, MessageSentEventArgs e)
        {
            Trace.WriteLine("Sent: " + e.Message);
        }

        void _client_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Trace.WriteLine(string.Format("Received:\r\n\tBtn : [{0}]\r\n\tCmd : [{1}]\r\n\tData: [{2}]\r\n\tSlot: [{3}]", e.Message.Button, e.Message.Command, e.Message.Data, e.Message.Slot));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ipendpoint = "192.168.10.5:4445"; 
            _client.Connect(ipendpoint);
            this.SetTrayStatus(string.Format("Connecting to {0}", ipendpoint));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _client.Disconnect();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _client.Authenticate("TESTA", "101", "1234");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _client.HeartbeatInterval = TimeSpan.FromSeconds(int.Parse(textBox1.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _client.Dial("0492390112");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.NotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.NotifyIcon.BalloonTipText = "Lorem ipsum dolor sit amet ipsum dolor sit amet, consectetur adipiscing elit. Ut convallis, erat eu fringilla accumsan, enim ante porta libero, id vulputate enim enim ornare ante. Etiam in lorem nulla. Donec tincidunt interdum urna sit amet condimentum. Aliquam erat volutpat.";
            this.NotifyIcon.BalloonTipTitle = "Lorem ipsum dolor sit amet";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.ShowBalloonTip(30 * 1000);
            this.NotifyIcon.BalloonTipClicked += (s, ea) => { Trace.Write("Balloon clicked"); };
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _hotkeymanager.Dispose();
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
