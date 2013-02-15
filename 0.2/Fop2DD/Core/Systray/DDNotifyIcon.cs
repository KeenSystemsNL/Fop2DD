using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using System;
using System.Windows.Forms;

namespace Fop2DD.Core.Systray
{
    public class DDNotifyIcon : IDDConnectionStateChangeNotifyable, IDisposable
    {
        private NotifyIcon _notifyicon;

        public ContextMenuStrip ContextMenuStrip
        {
            get { return _notifyicon.ContextMenuStrip; }
            set { _notifyicon.ContextMenuStrip = value; }
        }

        public DDNotifyIcon()
        {
            _notifyicon = new NotifyIcon();
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
