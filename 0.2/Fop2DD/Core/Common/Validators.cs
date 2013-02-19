using System;

namespace Fop2DD.Core.Common
{
    public static class Validators
    {
        public static bool IsValidHttpUrl(string value)
        {
            Uri result;
            if (Uri.TryCreate(value, UriKind.Absolute, out result))
            {
                return (result.Scheme == Uri.UriSchemeHttp)
                    || (result.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }

        public static bool IsValidPort(int value)
        {
            return (value > 0) && (value <= 65535);
        }
    }
}
