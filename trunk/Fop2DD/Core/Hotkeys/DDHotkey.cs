using GlobalHotKey;
using System;
using System.Linq;
using System.Windows.Input;

namespace Fop2DD.Core.Hotkeys
{
    public class DDHotkey
    {
        private static string KeySeparator = " + ";

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

        public override string ToString()
        {
            var modifiers =
                Enum.GetValues(typeof(ModifierKeys)).Cast<ModifierKeys>()
                .Where(k => ((int)k & (int)this.Modifier) != 0)
                .Select(k=> Enum.GetName(typeof(ModifierKeys), k));

            return string.Join(KeySeparator, modifiers) + KeySeparator + Enum.GetName(typeof(Key), this.Key);
        }

        public static DDHotkey Parse(string value)
        {
            var key = Key.None;
            var modifier = ModifierKeys.None;

            var parts = value.Split(new[] { KeySeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1)
            {
                for (int i = 0; i < parts.Length - 1; i++)
                    modifier |= (ModifierKeys)Enum.Parse(typeof(ModifierKeys), parts[i]);
            }
            if (parts.Length > 0)
                key = (Key)Enum.Parse(typeof(Key), parts[parts.Length-1]);

            return new DDHotkey(key, modifier);
        }
        
    }
}
