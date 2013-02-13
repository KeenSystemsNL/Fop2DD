using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Fop2DD
{
    public static class Iconhandler
    {
        private static Dictionary<string, Icon> _iconcache = new Dictionary<string, Icon>(StringComparer.OrdinalIgnoreCase);

        public static Icon LoadIcon(string filename)
        {
            if (!_iconcache.ContainsKey(filename))
            {
                try
                {
                    _iconcache.Add(filename, Icon.ExtractAssociatedIcon(Path.Combine("icons", filename + ".ico")));
                }
                catch (Exception)
                {
                    _iconcache.Add(filename, SystemIcons.Error);
                }
            }
            return _iconcache[filename];
        }
    }
}
