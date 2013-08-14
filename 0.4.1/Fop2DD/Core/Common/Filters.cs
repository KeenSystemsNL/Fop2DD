using System.Text.RegularExpressions;

namespace Fop2DD.Core.Common
{
    public static class Filters
    {
        //Regex to remove double (or more) whitespace
        private static Regex _removeextrawhitespace = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        //Regex to remove anything but numbers
        private static Regex _digitsonly = new Regex(@"([^0-9])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Removes any non-digits from a string.
        /// </summary>
        /// <param name="value">The value to remove non-digits from.</param>
        /// <returns>Returns the original string with anything but digits removed.</returns>
        public static string DigitsOnly(string value)
        {
            return _digitsonly.Replace(value ?? string.Empty, string.Empty);
        }

        /// <summary>
        /// Removes extra whitespace (double or more spaces/tabs etc.) from the string.
        /// </summary>
        /// <param name="value">The value to remove the extra whitespace from.</param>
        /// <returns>Returns the original string with extra whitespace removed.</returns>
        public static string RemoveExtraWhitespace(string value)
        {
            return _removeextrawhitespace.Replace(value ?? string.Empty, " ").Trim();
        }

        /// <summary>
        /// Normalizes a number like "+31123456789" to "0031123456789"
        /// </summary>
        /// <param name="value">The number to normalize.</param>
        /// <returns>Returns a normalized number.</returns>
        /// <remarks>
        ///     Currently this is a very simple routine that should be modified
        ///     to handle all sorts of normalization and take into account different
        ///     countries' rules.
        /// </remarks>
        public static string NormalizeNumber(string value)
        {
            return (value ?? string.Empty).Replace("+", "00");
        }
    }
}
