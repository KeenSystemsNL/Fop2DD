using System;
using System.Net.Sockets;

namespace Fop2ClientLib
{
    /// <summary>
    /// Specifies constants that define the outcomes of an <see cref="IFop2Client.Authenticate"/> call.
    /// </summary>
    public enum AuthenticationStatus
    {
        /// <summary>Authentication was successful.</summary>
        Success,
        /// <summary>Authentication failed.</summary>
        Failed
    }

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.AuthenticationResultReceived"/> event.
    /// </summary>
    public class AuthenticationResultReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="AuthenticationStatus"/> containing information about the authentication attempt.
        /// </summary>
        public AuthenticationStatus Result { get; private set; }

        /// <summary>
        /// Initializes a new AuthenticationResultReceivedEventArgs.
        /// </summary>
        /// <param name="result"><see cref="AuthenticationStatus"/> containing information about the authentication attempt.</param>
        internal AuthenticationResultReceivedEventArgs(AuthenticationStatus result)
        {
            this.Result = result;
        }
    }
}