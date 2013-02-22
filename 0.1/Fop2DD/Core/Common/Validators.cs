using System;

namespace Fop2DD.Core.Common
{
    public static class Validators
    {
        public const int PORT_MIN = 1;
        public const int PORT_MAX = 65535;
        public const int PING_MIN = 5;
        public const int PING_MAX = 600;
        public const int GRAB_MIN = 3;
        public const int GRAB_MAX = 15;
        public const int DIALCMD_MIN = 3;
        public const int DIALCMD_MAX = 15;

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
            return (value >= PORT_MIN) && (value <= PORT_MAX);
        }

        public static bool IsValidPingInterval(int value)
        {
            return (value >= PING_MIN) && (value <= PING_MAX);
        }

        public static bool IsValidGrabMinLength(int value)
        {
            return (value >= GRAB_MIN) && (value <= GRAB_MAX);
        }

        public static bool IsValidDialCmdMinLength(int value)
        {
            return (value >= DIALCMD_MIN) && (value <= DIALCMD_MAX);
        }
    }
}
