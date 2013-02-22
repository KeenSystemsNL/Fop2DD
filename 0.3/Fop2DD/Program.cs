using Fop2DD.Core;
using System;
using System.Threading;
using System.Windows.Forms;

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
                bool creatednew = false;
                var mux = new Mutex(true, "mux_" + Application.ProductName, out creatednew);
                if (creatednew)
                {
                    core.Start();

                    Application.Run();

                    core.Stop();

                    mux.ReleaseMutex();
                    mux.Close();
                }
                else
                {

                }
            }
        }
    }
}
