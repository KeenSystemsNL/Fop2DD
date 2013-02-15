using Fop2DD.Core.Common;
using GlobalHotKey;
using System;
using System.Collections.Generic;

namespace Fop2DD.Core.Hotkeys
{
    public delegate void DDHotKeyPressedEventHandler(object sender, DDHotkeyPressedEventArgs e);

    public class DDHotkeyManager : IDisposable
    {
        private HotKeyManager _hotkeymanager;
        private Dictionary<DDHotkeyType, DDHotkey> _hotkeys;
        private Dictionary<DDHotkey, DDHotkeyType> _hotkeytypelookup;
        private PhonenumberGrabber _phonenumbergrabber;

        public event DialRequestEventhandler DialRequest;

        public DDHotkeyManager()
        {
            _phonenumbergrabber = new PhonenumberGrabber();

            _hotkeymanager = new HotKeyManager();
            _hotkeys = new Dictionary<DDHotkeyType, DDHotkey>();
            _hotkeytypelookup = new Dictionary<DDHotkey, DDHotkeyType>();
            _hotkeymanager.KeyPressed += _hotkeymanager_KeyPressed;
        }

        private void _hotkeymanager_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            var kp = new DDHotkey(e.HotKey);
            if (_hotkeytypelookup.ContainsKey(kp))
            {
                switch (_hotkeytypelookup[kp])
                {
                    case DDHotkeyType.DialSelectionFromActiveWindow:
                        var numbers = _phonenumbergrabber.TryGrabPhonenumbersFromSelection(Properties.Settings.Default.GrabMinLength);
                        if (numbers.Length > 0)
                            DialRequest(this, new DialRequestEventArgs(numbers));
                        break;
                }
            }
        }

        public void Dispose()
        {
            _hotkeymanager.Dispose();
        }

        public void Register(DDHotkeyType type, DDHotkey hotkey)
        {
            this.UnregisterByType(type);
            this.UnregisterByKey(hotkey);

            _hotkeys.Add(type, hotkey);
            _hotkeytypelookup.Add(hotkey, type);
            _hotkeymanager.Register(hotkey.Key, hotkey.Modifier);
        }

        public void UnregisterByType(DDHotkeyType type)
        {
            if (_hotkeys.ContainsKey(type))
            {
                var k = _hotkeys[type];
                _hotkeytypelookup.Remove(k);
                _hotkeys.Remove(type);
                _hotkeymanager.Unregister(k.Key, k.Modifier);
            }
        }

        public void UnregisterByKey(DDHotkey hotkey)
        {
            if (_hotkeytypelookup.ContainsKey(hotkey))
                this.UnregisterByType(_hotkeytypelookup[hotkey]);
        }

    }

    public delegate void DialRequestEventhandler(object sender, DialRequestEventArgs e);

    public class DialRequestEventArgs : EventArgs
    {
        public string[] Numbers { get; private set; }

        public DialRequestEventArgs(string[] numbers)
        {
            this.Numbers = numbers;
        }
    }
}
