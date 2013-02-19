using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
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
        /// Executes a <see cref="ShellCommand"/> replacing the placeholders with the specified value.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="replacements">The placeholders and their values to replace them with.</param>
        public static void ExecuteCommand(ShellCommand command, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            try
            {
                Process.Start(command.ToProcessStartInfo(replacements));
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
        /// <param name="useurlencoding">When true, all replacements will be URL encoded.</param>
        /// <returns>Returns the value with all placeholders (if any) replaced with their respective replacement.</returns>
        public static string ReplacePlaceholder(string value, IEnumerable<KeyValuePair<string, string>> replacements, bool useurlencoding)
        {
            if (replacements != null)
            {
                foreach (var kv in replacements)
                    value = ReplacePlaceholder(value, kv.Key, kv.Value, useurlencoding);
            }
            return value;
        }

        /// <summary>
        /// Replaces the placeholder(s) with a value.
        /// </summary>
        /// <param name="value">The string to replace placeholders is.</param>
        /// <param name="placeholder">The placeholder to replace.</param>
        /// <param name="replacement">The replacement value</param>
        /// <param name="useurlencoding">When true, all replacements will be URL encoded.</param>
        /// <returns>Returns the value with all placeholders (if any) replaced with the replacement.</returns>
        public static string ReplacePlaceholder(string value, string placeholder, string replacement, bool useurlencoding)
        {

            return new Regex(Regex.Escape(placeholder), RegexOptions.CultureInvariant | RegexOptions.IgnoreCase).Replace(value, useurlencoding ? HttpUtility.UrlEncode(replacement) : replacement);
        }
    }
}
