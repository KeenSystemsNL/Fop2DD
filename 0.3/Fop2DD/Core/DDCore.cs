using Fop2ClientLib;
using Fop2DD.Core.Common;
using Fop2DD.Core.Connection;
using Fop2DD.Core.Hotkeys;
using Fop2DD.Core.Systray;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Fop2DD.Core
{
    public class DDCore : IDisposable
    {
        private Fop2FatClient _client;
        private DDConnectionManager _connectionmanager;
        private DDHotkeyManager _hotkeymanager;
        private DDNotifyIcon _notifyicon;

        private ToolStripMenuItem _fop2webinterface;

        public DDCore()
        {
            _client = new Fop2FatClient();
            _hotkeymanager = new DDHotkeyManager();
            _connectionmanager = new DDConnectionManager(_client);

            _fop2webinterface = new ToolStripMenuItem(Properties.Resources.menu_fop2web, Iconhandler.LoadIconAsImage("world_go"), event_WebInterface);

            _notifyicon = new DDNotifyIcon(_client);
            _notifyicon.ContextMenuStrip = DDNotifyIcon.CreateDefaultContextMenu(event_OnSettings, event_OnAbout, event_OnExit);
            _notifyicon.ContextMenuStrip.Items.Insert(0, _fop2webinterface);
            _notifyicon.ContextMenuStrip.Opening += event_ContextMenuStripOpening;

            _notifyicon.BalloonClicked += event_BalloonClicked;
            _hotkeymanager.DialRequest += event_DialRequest;

            _connectionmanager.RegisterListener(_notifyicon);
        }

        public void DialFromCommandlineArgs(string[] args)
        {
            var number = GetNumberFromArgs(args);
            if (number != null)
                _client.Dial(number);
        }

        private string GetNumberFromArgs(string[] args)
        {
            return args.Select(a => Filters.NumbersOnly(a))
                            .Where(n => n.Length > Properties.Settings.Default.GrabMinLength)
                            .FirstOrDefault();
        }

        private void event_ContextMenuStripOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _fop2webinterface.Enabled = (_client.IsAuthenticated && Validators.IsValidHttpUrl(Properties.Settings.Default.FOP2Url));
        }

        private void event_BalloonClicked(object sender, DDBalloonClickedEventArgs e)
        {
            var s = Properties.Settings.Default;

            if (!string.IsNullOrEmpty(s.DialCmd_File))
            {
                var phonenumber = Filters.NumbersOnly(e.BalloonInfo.CallerIdNumber);
                var command = new ShellCommand(s.DialCmd_File, s.DialCmd_Args);
                command.WorkingDirectory = s.DialCmd_WorkDir;

                ShellExecutor.ExecuteCommand(command, new[] {
                        new KeyValuePair<string, string>("%PHONENUMBER%", phonenumber)
                    });
            }
        }

        private void event_WebInterface(object sender, EventArgs e)
        {
            var command = new ShellCommand(Properties.Settings.Default.FOP2Url);

            ShellExecutor.ExecuteCommand(command, new[] {
                new KeyValuePair<string, string>("%CONTEXT%", _client.Context),
                new KeyValuePair<string, string>("%USER%", _client.Username),
                new KeyValuePair<string, string>("%PASS%", _client.Password),
            });
        }

        private void event_OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void event_OnAbout(object sender, EventArgs e)
        {
            using (var f = new AboutForm())
                f.ShowDialog();
        }

        private void event_OnSettings(object sender, EventArgs e)
        {
            using (var f = new SettingsForm())
            {
                f.SettingsChanged += (s, scea) =>
                {
                    _connectionmanager.Reconnect(DDCore.GetConnectionInfo());
                };
                f.ShowDialog();
            }
        }

        private void event_DialRequest(object sender, DialRequestEventArgs e)
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
                numbertodial = Filters.NumbersOnly(numbertodial);
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