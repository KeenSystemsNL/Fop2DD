using System;

namespace Fop2ClientLib
{
    /// <summary>
    /// Provides data for the <see cref="IFop2Client.HeartbeatReceived"/> event.
    /// </summary>
    public class HeartbeatReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the data that was sent along a heartbeat response (e.g. "pong")
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Initializes a new HeartbeatReceivedEventArgs.
        /// </summary>
        /// <param name="data">Data associated with the HeartbeatReceivedEventArgs.</param>
        internal HeartbeatReceivedEventArgs(string data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.HeartbeatSent"/> event.
    /// </summary>
    public class HeartbeatSentEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new HeartbeatSentEventArgs.
        /// </summary>
        internal HeartbeatSentEventArgs()
        {
        }
    }
}
