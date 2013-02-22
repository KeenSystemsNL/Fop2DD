using System;
using System.Net.Sockets;

namespace Fop2ClientLib
{
    /// <summary>
    /// Specifies constants that define the conection state.
    /// </summary>
    public enum ConnectionState
    {
        /// <summary>Client is connected.</summary>
        Connected,
        /// <summary>Client is disconnected.</summary>
        Disconnected
    }

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.ConnectionStateChanged"/> event.
    /// </summary>
    public class ConnectionStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="ConnectionState"/> containing information about the connection status.
        /// </summary>
        public ConnectionState ConnectionState { get; private set; }

        /// <summary>
        /// Initializes a new ConnectionStateChangedEventArgs.
        /// </summary>
        /// <param name="connectionstate"><see cref="ConnectionState"/> containing information about the connection status.</param>
        internal ConnectionStateChangedEventArgs(ConnectionState connectionstate)
        {
            this.ConnectionState = connectionstate;
        }
    }
}