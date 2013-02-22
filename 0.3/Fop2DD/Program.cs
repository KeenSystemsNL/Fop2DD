using Fop2DD.Core;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Fop2DD
{
    static class Program
    {
        private static DDCore _core;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!ApplicationInstanceManager.CreateSingleInstance(Assembly.GetExecutingAssembly().GetName().Name, SingleInstanceCallback))
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (_core = new DDCore())
            {
                _core.Start();

                Application.Run();

                _core.Stop();
            }
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (args == null || _core == null) return;
            _core.DialFromCommandlineArgs(args.CommandLineArgs);
        }
    }
}
