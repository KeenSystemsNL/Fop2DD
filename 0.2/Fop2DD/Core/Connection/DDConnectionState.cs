using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fop2DD.Core.Connection
{
    public enum DDConnectionState
    {
        Connected,
        ConnectionLost,
        ConnectionTimedOut,
        AuthenticationFailed,
        AuthenticationSucceeded
    }
}