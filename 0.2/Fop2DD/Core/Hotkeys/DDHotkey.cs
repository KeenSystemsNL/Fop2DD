using GlobalHotKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Fop2DD.Core.Hotkeys
{
    public class DDHotkey
    {
        public Key Key { get; set; }
        public ModifierKeys Modifier { get; set; }

        public DDHotkey(Key key, ModifierKeys modifier)
        {
            this.Key = key;
            this.Modifier = modifier;
        }

        internal DDHotkey(HotKey hotkey)
            : this(hotkey.Key, hotkey.Modifiers) { }

        public override int GetHashCode()
        {
            return this.Key.GetHashCode() ^ this.Modifier.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DDHotkey))
                return false;

            return this.Key.Equals(((DDHotkey)obj).Key) && this.Modifier.Equals(((DDHotkey)obj).Modifier);
        }
    }
}
