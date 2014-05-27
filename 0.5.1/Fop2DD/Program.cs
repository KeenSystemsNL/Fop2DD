using Fop2DD.Core;
using Fop2DD.Core.IPC;
using Fop2DD.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Fop2DD
{
    static class Program
    {
        private static DDCore _core;

        private const string appid = "9F94F10A-CAEB-4DC5-B1F1-C6001C1B4D91";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // http://odetocode.com/blogs/scott/archive/2004/08/20/the-misunderstood-mutex.aspx
            using (var mux = new Mutex(false, appid))
            {
                //If we can acquire the mutex...
                if (mux.WaitOne(0, false))
                {
                    //...we're the first instance running (in this session (think of terminal sessions!))

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
                }
                else
                {
                    //There's already an instance running; pass along our commandline args
                    DDCore.SendIPCPessage(string.Join("|", args));
                }
            }
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
