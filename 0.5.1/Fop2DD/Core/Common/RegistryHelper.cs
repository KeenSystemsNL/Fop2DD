using System.Windows.Forms;
using Microsoft.Win32;

namespace Fop2DD.Core.Common
{
    public class RegistryHelper
    {
        private const string _runpath = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        private static RegistryKey GetOrCreateKey(string Path)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(Path, true);
            if (Key == null)
                Key = Registry.CurrentUser.CreateSubKey(Path);
            return Key;
        }

        private static RegistryKey _runkey;
        public static RegistryKey RunKey
        {
            get
            {
                if (_runkey == null)
                    _runkey = GetOrCreateKey(_runpath);
                return _runkey;
            }
        }
        
        public static bool IsInRunKey()
        {
            return RegistryHelper.RunKey.GetValue(Application.ProductName) != null;
        }

        public static void AddToRunKey()
        {
            RegistryHelper.RunKey.SetValue(Application.ProductName, Application.ExecutablePath);
        }

        public static void RemoveFromRunKey()
        {
            RegistryHelper.RunKey.DeleteValue(Application.ProductName);
        }

    }
}
