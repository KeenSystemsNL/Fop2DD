using Fop2ClientLib;
using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using System;
using System.Windows.Forms;

namespace Fop2DD.Core.Systray
{
    public delegate void BalloonClickedEventHandler(object sender, DDBalloonClickedEventArgs e);

    public class DDNotifyIcon : IDDConnectionStateChangeNotifyable, IDisposable
    {
        private NotifyIcon _notifyicon;
        private IFop2Client _client;

        private string _lastclidname;
        private string _lastclidnum;

        private DDBalloonInfo _currentballoon;

        public event BalloonClickedEventHandler BalloonClicked;

        public ContextMenuStrip ContextMenuStrip
        {
            get { return _notifyicon.ContextMenuStrip; }
            set { _notifyicon.ContextMenuStrip = value; }
        }

        public DDNotifyIcon(IFop2Client client)
        {
            _notifyicon = new NotifyIcon();

            _notifyicon.BalloonTipClosed += (s, e) => { _currentballoon = null; };
            _notifyicon.BalloonTipClicked += (s, e) =>
            {
                if ((_currentballoon != null) && (BalloonClicked != null)) 
                    BalloonClicked(this, new DDBalloonClickedEventArgs(_currentballoon));
            };

            _client = client;

            _client.MessageReceived += MessageReceived;
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //TODO: {0}@{1} (or id@context) should probably be some property/method on Fop2Client for easy usage
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
                            if (_lastclidnum.Length >= Properties.Settings.Default.DialCmd_MinLength)
                            {
                                _notifyicon.ShowBalloonTip(
                                    (int)TimeSpan.FromSeconds(30).TotalMilliseconds,
                                    string.Format(Properties.Resources.balloon_title),
                                    string.Format(string.Format(Properties.Resources.balloon_text, _lastclidname, _lastclidnum)),
                                    ToolTipIcon.Info
                                    );

                                _currentballoon = new DDBalloonInfo(_lastclidname, _lastclidnum);

                                _lastclidname = null;
                                _lastclidnum = null;
                            }
                        }
                        break;
                }
            }
        }

        public void StateChanged(object sender, DDConnectionStateChangedEventArgs e)
        {
            NotifyInfo ni = null;
            switch (e.State)
            {
                case DDConnectionState.Connected:
                    ni = new NotifyInfo("online", Properties.Resources.status_online);
                    break;
                case DDConnectionState.ConnectionLost:
                    ni = new NotifyInfo("offline", Properties.Resources.status_offline);
                    break;
                case DDConnectionState.ConnectionTimedOut:
                    ni = new NotifyInfo("error", string.Format(Properties.Resources.status_error, e.Data));
                    break;
                case DDConnectionState.AuthenticationFailed:
                    ni = new NotifyInfo("authfailure", Properties.Resources.status_authfailure);
                    break;
                case DDConnectionState.AuthenticationSucceeded:
                    ni = new NotifyInfo("online", Properties.Resources.status_authsuccess);
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
                new ToolStripMenuItem(Properties.Resources.menu_settings, Iconhandler.LoadIconAsImage("wrench"), onsettings),
                new ToolStripSeparator(),
                new ToolStripMenuItem(Properties.Resources.menu_about, Iconhandler.LoadIconAsImage("information"), onabout),
                new ToolStripMenuItem(Properties.Resources.menu_exit, Iconhandler.LoadIconAsImage("cross"), onexit),
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
