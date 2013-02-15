﻿using System;
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
}
