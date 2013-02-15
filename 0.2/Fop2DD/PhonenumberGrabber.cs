using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fop2DD
{
    // See https://gist.github.com/RobThree/4943822 for updates / forks etc. and possibly improvements.

    /// <summary>
    ///     Tries to grab phonenumbers from an active control (selected text) in an active window. Uses
    ///     a <see cref="TextSelectionReader"/> internally.
    /// </summary>
    public class PhonenumberGrabber
    {
        //Regex to remove double (or more) whitespace
        private static Regex removewhitespace = new Regex(@"\s{2,}", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
        //Regex to remove anything but numbers
        private static Regex numericonly = new Regex(@"([^0-9])", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        //Internal TextSelectionReader
        private TextSelectionReader _textselectionreader;

        /// <summary>
        /// Initializes a new instance of the PhonenumberGrabber
        /// </summary>
        public PhonenumberGrabber()
        {
            _textselectionreader = new TextSelectionReader();
        }

        /// <summary>
        ///     Tries to grab phonenumbers from the current selection in an active window and returns anything
        ///     that looks like a phonenumber.
        /// </summary>
        /// <param name="minlength">The minimum number of digits to match to consider something a phonenumber.</param>
        /// <returns>
        ///     Returns an array of strings that are found in the current selection in an active window that
        ///     match the criteria of looking like a phonenumber and being of a minimum given length.
        /// </returns>
        /// <remarks>
        ///     The matching of phonenumbers isn't very sophisticated in this version; a more sophisticated method
        ///     might be implemented peeking at Google's libphonenumber findNumbers methods (see
        ///     <a href="http://code.google.com/p/libphonenumber/source/search?q=findNumbers&origq=findNumbers">here</a>).
        /// </remarks>
        public string[] TryGrabPhonenumbersFromSelection(int minlength)
        {
            return _textselectionreader.TryGetSelectedTextFromActiveControl<string>((v) => { return FilterPhoneNumbers(v, minlength); })
                .OrderBy(n => n.Length)
                .ThenBy(n => n)
                .ToArray();
        }

        /// <summary>
        ///     Internal method that gets passed a string (current selection) and extracts phonenumbers from this text.
        /// </summary>
        /// <param name="value">The text to look for phonenumbers in.</param>
        /// <param name="minlength">The minimum length of returned phonenumbers.</param>
        /// <returns>
        ///     Returns extracted phonenumbers from the given text.
        /// </returns>
        /// <remarks>
        ///     This methods searches each line (Cr, Lf or CrLf separator) separately.
        /// </remarks>
        private static string[] FilterPhoneNumbers(string value, int minlength)
        {
            if (value == null)
                return new string[0];

            var lines = value.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Regex nummatch = new Regex(string.Format(@"([+0-9\s\(\)-]{{{0},}})", minlength), RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.Singleline);
            return lines.SelectMany(
                l => nummatch.Matches(l)
                    .Cast<Match>()
                    .Select(m => removewhitespace.Replace(m.Captures[0].Value, " ").Trim())
                    .Where(r => r.Length >= minlength)
                )
                .Select(n => numericonly.Replace(n.Replace("+", "00"), string.Empty))
                .ToArray();
        }
    }
}
