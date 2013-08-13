using Fop2ClientLib;
using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using Fop2DD.Core.Logging;
using System;
using System.Drawing;
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

        private IDDLogger logger = DDLogManager.GetLogger(typeof(DDNotifyIcon));

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
            _client.MessageSent += MessageSent;
        }

        private void MessageSent(object sender, MessageSentEventArgs e)
        {
            logger.LogDebug("Message sent:\n\t{0}", e.Message);
        }

        public void MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            logger.LogDebug("Message received:\n\tButton\t: {0}\n\tCommand\t: {1}\n\tData\t: {2}\n\tSlot\t: {3}", 
                e.Message.Button, e.Message.Command, e.Message.Data, e.Message.Slot);

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
            logger.LogDebug("State changed: {0}", e.State);

            NotifyInfo ni = null;
            switch (e.State)
            {
                case DDConnectionState.Connected:
                    ni = new NotifyInfo(Properties.Resources.ico_online, Properties.Resources.status_online);
                    break;
                case DDConnectionState.ConnectionLost:
                    ni = new NotifyInfo(Properties.Resources.ico_offline, Properties.Resources.status_offline);
                    break;
                case DDConnectionState.ConnectionTimedOut:
                    ni = new NotifyInfo(Properties.Resources.ico_error, string.Format(Properties.Resources.status_error, e.Data));
                    break;
                case DDConnectionState.AuthenticationFailed:
                    ni = new NotifyInfo(Properties.Resources.ico_authfailure, Properties.Resources.status_authfailure);
                    break;
                case DDConnectionState.AuthenticationSucceeded:
                    ni = new NotifyInfo(Properties.Resources.ico_online, Properties.Resources.status_authsuccess);
                    break;
            }

            if (ni != null)
                this.SetStatus(ni);
        }

        private void SetStatus(NotifyInfo notifyinfo)
        {
            if (_notifyicon.Visible == false)
                _notifyicon.Visible = true;

            _notifyicon.Icon = notifyinfo.Icon;
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
                new ToolStripMenuItem(Properties.Resources.menu_settings, Properties.Resources.ico_wrench.ToBitmap(), onsettings),
                new ToolStripSeparator(),
                new ToolStripMenuItem(Properties.Resources.menu_about, Properties.Resources.ico_information.ToBitmap(), onabout),
                new ToolStripMenuItem(Properties.Resources.menu_exit, Properties.Resources.ico_cross.ToBitmap(), onexit),
            };

            var m = new ContextMenuStrip();
            m.Items.AddRange(menuitems);
            return m;
        }

        private class NotifyInfo
        {
            public Icon Icon { get; set; }
            public string Text { get; set; }

            public NotifyInfo(Icon icon, string text)
            {
                this.Icon = icon;
                this.Text = text;
            }
        }
    }
}
