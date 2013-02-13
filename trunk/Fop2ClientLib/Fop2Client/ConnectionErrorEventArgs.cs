using System;
using System.Net.Sockets;

namespace Fop2ClientLib
{
    /// <summary>
    /// Provides data for the <see cref="IFop2Client.ConnectionError"/> event.
    /// </summary>
    public class ConnectionErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the exception containing information about the specific cause of the error
        /// </summary>
        public SocketException Exception { get; private set; }

        /// <summary>
        /// Initializes a new ConnectionErrorEventArgs.
        /// </summary>
        /// <param name="exception">Exception containing information about the specific cause of the error.</param>
        internal ConnectionErrorEventArgs(SocketException exception)
        {
            this.Exception = exception;
        }
    }
}
