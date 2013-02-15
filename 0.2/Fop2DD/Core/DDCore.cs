using Fop2ClientLib;
using Fop2DD.Core.Connection;
using Fop2DD.Core.Hotkeys;
using Fop2DD.Core.Systray;
using System;
using System.Net;
using System.Windows.Forms;
using System.Windows.Input;

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
            _notifyicon = new DDNotifyIcon();
            _notifyicon.ContextMenuStrip = DDNotifyIcon.CreateDefaultContextMenu(event_OnSettings, event_OnAbout, event_OnExit);

            //TODO: move this elsewhere... and make it configurable
            _hotkeymanager.Register(DDHotkeyType.DialSelectionFromActiveWindow, new DDHotkey(Key.F8, ModifierKeys.Control));

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

            //TODO: Ensure only (and ONLY) digits in number

            if (numbertodial != null)
                _client.Dial(numbertodial);
        }

        public void Start()
        {
            //Get connection info and connect if possible
            DDConnectionInfo c = new DDConnectionInfo(new IPEndPoint(IPAddress.Parse("192.168.10.5"), 4445), new DDCredential("TESTA", "101", "1234"));
            _connectionmanager.Connect(c);
        }

        public void Stop()
        {
            _connectionmanager.Disconnect();
            _client = null;
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