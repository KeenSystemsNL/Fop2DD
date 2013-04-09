using System;

namespace Fop2ClientLib
{
    /// <summary>
    /// Provides data for the <see cref="IFop2Client.Heartbeat"/> event.
    /// </summary>
    public class HeartbeatEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the data that was sent along a heartbeat response (e.g. "pong")
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Initializes a new HeartbeatEventArgs.
        /// </summary>
        /// <param name="data">Data associated with the HeartbeatEventArgs.</param>
        internal HeartbeatEventArgs(string data)
        {
            this.Data = data;
        }
    }
}
