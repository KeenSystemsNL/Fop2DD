using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Fop2DD.Core.Common
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
                    _iconcache.Add(filename, Icon.ExtractAssociatedIcon(Path.Combine("icons", string.Format("{0}.ico", filename))));
                }
                catch (Exception)
                {
                    _iconcache.Add(filename, SystemIcons.Error);
                }
            }
            return _iconcache[filename];
        }

        public static Image LoadIconAsImage(string filename)
        {
            return Iconhandler.LoadIcon(filename).ToBitmap();
        }
    }
}
