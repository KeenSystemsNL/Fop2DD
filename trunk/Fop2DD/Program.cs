using Fop2DD.Core;
using Fop2DD.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
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
            if (!ApplicationInstanceManager.CreateSingleInstance(Application.ProductName, SingleInstanceCallback))
                return;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            bool creatednew = false;
            var mux = new Mutex(true, "mux_" + Application.ProductName.ToLowerInvariant(), out creatednew);

            //When a json file was passed and we can find the file try to import it as default settings
            var importsettingsfile = args.Where(a => a.EndsWith(".json", StringComparison.OrdinalIgnoreCase) && File.Exists(a)).FirstOrDefault();
            if (importsettingsfile != null)
                ImportSettings(importsettingsfile);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (_core = new DDCore())
            {
                _core.Start();

                Application.Run();

                _core.Stop();
            }

            mux.ReleaseMutex();
            mux.Close();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = e.ExceptionObject as Exception;
                if (ex != null)
                    DDLogManager.GetLogger(typeof(Program)).LogFatal(ex);
            }
            finally
            {
                Application.Exit();
            }
        }

        private static void SingleInstanceCallback(object sender, InstanceCallbackEventArgs args)
        {
            if (args == null || _core == null) return;
            _core.DialFromCommandlineArgs(args.CommandLineArgs);
        }

        private static void ImportSettings(string path)
        {
            try
            {
                var f = JsonConvert.DeserializeObject<Properties.Settings>(File.OpenText(path).ReadToEnd());
                foreach (var x in f.Properties.OfType<SettingsProperty>())
                {
                    var pi = Properties.Settings.Default.GetType().GetProperty(x.Name);
                    var v = f.GetType().GetProperty(x.Name).GetValue(f, null);
                    pi.SetValue(Properties.Settings.Default, v, null);
                }
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error importing settings '{0}'", path), ex);
            }
        }
    }
}
