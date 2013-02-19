using System.Collections.Generic;
using System.Diagnostics;

namespace Fop2DD.Core.Common
{
    /// <summary>
    /// Represents a command that can be executed.
    /// </summary>
    public class ShellCommand
    {
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
        /// <param name="replacements">
        ///     The template values to be replaced (if any).
        /// </param>
        /// <returns>
        ///     Returns a "ready to use" <see cref="ProcessStartInfo"/> object that can be passed
        ///     to <see cref="Process.Start()"/>.
        /// </returns>
        /// <remarks>
        ///     Both the <see cref="Filename"/> and <see cref="Arguments"/> will be scanned for 
        ///     placeholder(s) which will be replaced by the values passed to this method
        /// </remarks>
        public ProcessStartInfo ToProcessStartInfo(IEnumerable<KeyValuePair<string, string>> replacements)
        {
            ProcessStartInfo pi = new ProcessStartInfo();

            pi.FileName = ShellExecutor.ReplacePlaceholder(this.Filename, replacements, Validators.IsValidHttpUrl(this.Filename));
            pi.Arguments = ShellExecutor.ReplacePlaceholder(this.Arguments, replacements, false);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = this.WorkingDirectory;
            return pi;
        }
    }
}
