
namespace Fop2DD.Core.Hotkeys
{
    public class DDHotkeyPressedEventArgs
    {
        public DDHotkeyType Type { get; private set; }
        public DDHotkey Hotkey { get; private set; }

        public DDHotkeyPressedEventArgs(DDHotkeyType type, DDHotkey hotkey)
        {
            this.Type = type;
            this.Hotkey = hotkey;
        }
    }
}
