
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