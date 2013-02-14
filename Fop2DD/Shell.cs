using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Fop2DD
{
    /// <summary>
    /// Used to fire up processes
    /// </summary>
    public static class ShellExecutor
    {
        /// <summary>
        /// Executes a <see cref="ShellCommand"/> replacing a placeholder with the specified phonenumber.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="phonenumber">The phonenumber to put in place of any placeholders.</param>
        public static void ExecuteCommand(ShellCommand command, string phonenumber)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            try
            {
                Process.Start(command.ToProcessStartInfo(phonenumber));
            }
            catch (Exception ex)
            {
                //For now we just alert the user; maybe someday we'll use something more sophisticated than a messagebox
                MessageBox.Show(string.Format("{0}\r\n[{1}]", ex.Message, ex.GetType().Name), "Error executing command", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Represents a command that can be executed.
    /// </summary>
    public class ShellCommand
    {
        //Used to filter everything but digits from shell commands when passing a phonenumber
        private static Regex _numericonly = new Regex("[^0-9]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        //Used to replace placeholders (case-insensitive)
        private static Regex _placeholder = new Regex(Regex.Escape(PLACEHOLDER), RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        //The placeholder to replace
        private const string PLACEHOLDER = "%PHONENUMBER%";

        /// <summary>
        /// Gets or sets the application or document to start.
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Gets or sets the directory that contains the process to be started.
        /// </summary>
        public string WorkingDirectory { get; set; }
        /// <summary>
        /// Gets or sets the set of command-line arguments to use when starting the application.
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        ///     Initializes a new instance of the ShellCommand class without specifying a file name
        ///     with which to start the process.
        /// </summary>
        public ShellCommand()
            : this(string.Empty) { }

        /// <summary>
        ///     Initializes a new instance of the ShellCommand class and specifies a file name such
        ///     as an application or document with which to start the process.
        /// </summary>
        public ShellCommand(string filename)
            : this(filename, string.Empty) { }

        /// <summary>
        ///     Initializes a new instance of the ShellCommand class and specifies a file name such
        ///     as an application or document with which to start the process and the arguments to pass.
        /// </summary>
        public ShellCommand(string filename, string arguments)
        {
            this.Filename = filename;
            this.Arguments = arguments;
            this.WorkingDirectory = string.Empty;
        }


        /// <summary>
        ///     Returns a "ready to use" <see cref="ProcessStartInfo"/> object that can be passed
        ///     to <see cref="Process.Start()"/>.
        /// </summary>
        /// <param name="phonenumber">
        ///     The phonenumber to be used for the placeholder(s) (if any)
        /// </param>
        /// <returns>
        ///     Returns a "ready to use" <see cref="ProcessStartInfo"/> object that can be passed
        ///     to <see cref="Process.Start()"/>.
        /// </returns>
        /// <remarks>
        ///     Both the <see cref="Filename"/> and <see cref="Arguments"/> will be scanned for 
        ///     placeholder(s) which will be replaced by the phonenumber passed to this method
        /// </remarks>
        public ProcessStartInfo ToProcessStartInfo(string phonenumber)
        {
            //Ensure phonenumber only contains digits
            phonenumber = _numericonly.Replace(phonenumber ?? string.Empty, string.Empty);

            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = ShellCommand.ReplacePhonenumberArgument(this.Filename, phonenumber);
            pi.Arguments = ShellCommand.ReplacePhonenumberArgument(this.Arguments, phonenumber);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = this.WorkingDirectory;
            return pi;
        }

        /// <summary>
        /// Replaces the placeholder(s) with the actual phonenumber.
        /// </summary>
        /// <param name="value">The value to scanned for placeholder(s).</param>
        /// <param name="phonenumber">The phonenumber to be used in place of the placeholder(s).</param>
        /// <returns>Returns the value with all placeholders (if any) replaced with the phonenumber</returns>
        private static string ReplacePhonenumberArgument(string value, string phonenumber)
        {
            return _placeholder.Replace(value, phonenumber);
        }
    }
}
