using Fop2ClientLib;
using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using Fop2DD.Core.Hotkeys;
using Fop2DD.Core.Systray;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Fop2DD.Core
{
    public class DDCore : IDisposable
    {
        private Fop2FatClient _client;
        private DDConnectionManager _connectionmanager;
        private DDHotkeyManager _hotkeymanager;
        private DDNotifyIcon _notifyicon;

        public DDCore()
        {
            _client = new Fop2FatClient();
            _hotkeymanager = new DDHotkeyManager();
            _connectionmanager = new DDConnectionManager(_client);
            _notifyicon = new DDNotifyIcon(_client);
            _notifyicon.ContextMenuStrip = DDNotifyIcon.CreateDefaultContextMenu(event_OnSettings, event_OnAbout, event_OnExit);

            _hotkeymanager.DialRequest += event_DialRequest;

            _connectionmanager.RegisterListener(_notifyicon);
        }

        public void event_OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void event_OnAbout(object sender, EventArgs e)
        {
            using (var f = new AboutForm())
                f.ShowDialog();
        }

        public void event_OnSettings(object sender, EventArgs e)
        {
            using (var f = new SettingsForm())
                f.ShowDialog();
        }

        public void event_DialRequest(object sender, DialRequestEventArgs e)
        {
            if (e.Numbers.Length == 0)
                return;

            string numbertodial = null;
            if (e.Numbers.Length > 1)
            {
                using (var f = new SelectNumberForm(e.Numbers))
                    if (f.ShowDialog() == DialogResult.OK)
                        numbertodial = f.SelectedNumber;
            }
            else
            {
                numbertodial = e.Numbers[0];
            }

            if (numbertodial != null)
            {
                numbertodial = PhonenumberGrabber.StripNonDigit(numbertodial);
                if (!string.IsNullOrWhiteSpace(numbertodial))
                    _client.Dial(numbertodial);
            }
        }

        public void Start()
        {
            _hotkeymanager.Register(DDHotkeyType.DialSelectionFromActiveWindow, DDHotkey.Parse(Properties.Settings.Default.GlobalDialHotkey));
            _connectionmanager.Connect(DDCore.GetConnectionInfo());
        }

        public void Stop()
        {
            _hotkeymanager.UnregisterByType(DDHotkeyType.DialSelectionFromActiveWindow);
            _connectionmanager.Disconnect();
        }

        public static DDConnectionInfo GetConnectionInfo()
        {
            var s = Properties.Settings.Default;
            return new DDConnectionInfo(
                new DDFop2Endpoint(s.Host, s.Port),
                new DDCredential(s.PBXContext, s.Username, s.Password),
                TimeSpan.FromSeconds(s.PingInterval),
                TimeSpan.FromSeconds(s.ConnectTimeout)
                );
        }

        #region IDisposable
        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _notifyicon.Dispose();
                _hotkeymanager.Dispose();
            }

            // free native resources if there are any.
        }
        #endregion
    }
}