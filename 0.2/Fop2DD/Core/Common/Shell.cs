using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Fop2DD.Core.Common
{
    /// <summary>
    /// Used to fire up processes
    /// </summary>
    public static class ShellExecutor
    {
        /// <summary>
        /// Executes a <see cref="ShellCommand"/>.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        public static void ExecuteCommand(ShellCommand command)
        {
            ExecuteCommand(command, null);
        }

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

        /// <summary>
        /// Replaces the placeholder(s) with a value.
        /// </summary>
        /// <param name="value">The string to replace placeholders is.</param>
        /// <param name="replacements">The placeholder/value pairs to replace.</param>
        /// <returns>Returns the value with all placeholders (if any) replaced with their respective replacement.</returns>
        public static string ReplacePlaceholder(string value, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            foreach (var kv in replacements)
                value = ReplacePlaceholder(value, kv.Key, kv.Value);
            return value;
        }

        /// <summary>
        /// Replaces the placeholder(s) with a value.
        /// </summary>
        /// <param name="value">The string to replace placeholders is.</param>
        /// <param name="placeholder">The placeholder to replace.</param>
        /// <param name="replacement">The replacement value</param>
        /// <returns>Returns the value with all placeholders (if any) replaced with the replacement.</returns>
        public static string ReplacePlaceholder(string value, string placeholder, string replacement)
        {
            return new Regex(Regex.Escape(placeholder), RegexOptions.CultureInvariant | RegexOptions.IgnoreCase).Replace(value, replacement);
        }
    }

    /// <summary>
    /// Represents a command that can be executed.
    /// </summary>
    public class ShellCommand
    {
        //Used to filter everything but digits from shell commands when passing a phonenumber
        private static Regex _numericonly = new Regex("[^0-9]", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        //The placeholder to replace
        private const string PHONENUMBERPLACEHOLDER = "%PHONENUMBER%";

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
            return ShellExecutor.ReplacePlaceholder(value, PHONENUMBERPLACEHOLDER, phonenumber);
        }
    }
}
