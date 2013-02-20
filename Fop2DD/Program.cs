using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Fop2DD.Core;

namespace Fop2DD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var core = new DDCore())
            {
                core.Start();

                Application.Run();

                core.Stop();
            }
        }
    }
}
