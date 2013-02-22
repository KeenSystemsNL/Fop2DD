using System.Text.RegularExpressions;

namespace Fop2DD.Core.Common
{
    public static class Filters
    {
        //Regex to remove double (or more) whitespace
        private static Regex _removeextrawhitespace = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        //Regex to remove anything but numbers
        private static Regex _numericonly = new Regex(@"([^0-9])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Removes any non-digits from a string.
        /// </summary>
        /// <param name="value">The value to remove non-digits from.</param>
        /// <returns>Returns the original string with anything but digits removed.</returns>
        public static string NumbersOnly(string value)
        {
            return _numericonly.Replace(value ?? string.Empty, string.Empty);
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
    }
}
