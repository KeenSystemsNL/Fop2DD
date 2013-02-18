using Fop2ClientLib;
using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Fop2DD.Core.Systray
{
    public class DDNotifyIcon : IDDConnectionStateChangeNotifyable, IDisposable
    {
        private NotifyIcon _notifyicon;
        private IFop2Client _client;

        private string _lastclidname;
        private string _lastclidnum;

        public ContextMenuStrip ContextMenuStrip
        {
            get { return _notifyicon.ContextMenuStrip; }
            set { _notifyicon.ContextMenuStrip = value; }
        }

        public DDNotifyIcon(IFop2Client client)
        {
            _notifyicon = new NotifyIcon();
            _client = client;

            _client.MessageReceived += MessageReceived;
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.Message.Button.Equals(string.Format("{0}@{1}", _client.Id, _client.Context), StringComparison.OrdinalIgnoreCase))
            {
                switch (e.Message.Command.ToLowerInvariant())
                {
                    case "clidname":
                        _lastclidname = e.Message.Data;
                        break;
                    case "clidnum":
                        _lastclidnum = e.Message.Data;
                        break;
                    case "notifyringing":
                        if ((!string.IsNullOrEmpty(_lastclidnum)) && (_lastclidname != null))
                        {
                            _notifyicon.ShowBalloonTip(
                                (int)TimeSpan.FromSeconds(30).TotalMilliseconds,
                                string.Format("Incoming call"),
                                string.Format(string.Format("{0} ({1})", _lastclidname, _lastclidnum)),
                                ToolTipIcon.Info
                                );
                            _lastclidname = null;
                            _lastclidnum = null;
                        }
                        break;
                }
            }

            //Trace.WriteLine(string.Format("==================\r\n\t{0}\r\n\t{1}\r\n\t{2}\r\n\t{3}", e.Message.Command, e.Message.Data, e.Message.Button, e.Message.Slot));
        }

        public void StateChanged(object sender, DDConnectionStateChangedEventArgs e)
        {
            NotifyInfo ni = null;
            switch (e.State)
            {
                case DDConnectionState.Connected:
                    ni = new NotifyInfo("online", "Online");
                    break;
                case DDConnectionState.ConnectionLost:
                    ni = new NotifyInfo("offline", "Offline");
                    break;
                case DDConnectionState.ConnectionTimedOut:
                    ni = new NotifyInfo("error", string.Format("Error: {0}", e.Data));
                    break;
                case DDConnectionState.AuthenticationFailed:
                    ni = new NotifyInfo("authfailure", "Authentication failed");
                    break;
                case DDConnectionState.AuthenticationSucceeded:
                    ni = new NotifyInfo("online", "Online");
                    break;
            }

            if (ni != null)
                this.SetStatus(ni);
        }

        private void SetStatus(NotifyInfo notifyinfo)
        {
            if (_notifyicon.Visible == false)
                _notifyicon.Visible = true;

            _notifyicon.Icon = Iconhandler.LoadIcon(notifyinfo.Icon);
            var tooltip = (notifyinfo.Text ?? string.Empty);
            _notifyicon.Text = (tooltip.Length > 63) ? tooltip.Substring(0, 63) : tooltip;
        }

        public void Dispose()
        {
            _notifyicon.Dispose();
        }

        public static ContextMenuStrip CreateDefaultContextMenu(EventHandler onsettings, EventHandler onabout, EventHandler onexit)
        {
            var menuitems = new ToolStripItem[] {
                new ToolStripMenuItem("Settings", Iconhandler.LoadIconAsImage("wrench"), onsettings),
                new ToolStripSeparator(),
                new ToolStripMenuItem("About", Iconhandler.LoadIconAsImage("information"), onabout),
                new ToolStripMenuItem("Exit", Iconhandler.LoadIconAsImage("cross"), onexit),
            };

            var m = new ContextMenuStrip();
            m.Items.AddRange(menuitems);
            return m;
        }

        private class NotifyInfo
        {
            public string Icon { get; set; }
            public string Text { get; set; }

            public NotifyInfo(string icon, string text)
            {
                this.Icon = icon;
                this.Text = text;
            }
        }
    }
}
