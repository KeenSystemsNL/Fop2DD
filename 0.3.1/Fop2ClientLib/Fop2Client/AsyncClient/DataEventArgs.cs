using System;

namespace Fop2ClientLib
{
    /// <summary>
    /// Provides a baseclass for the <see cref="DataReceivedEventArgs"/> and <see cref="DataSentEventArgs"/>.
    /// </summary>
    internal abstract class DataEventArgs : EventArgs
    {
        public string Data { get; private set; }

        public DataEventArgs(string data)
        {
            this.Data = data;
        }
    }

    /// <summary>
    /// Provides data for the <see cref="IAsyncClient.DataReceived"/> event.
    /// </summary>
    internal class DataReceivedEventArgs : DataEventArgs
    {
        public DataReceivedEventArgs(string data)
            : base(data) { }
    }

    /// <summary>
    /// Provides data for the <see cref="IAsyncClient.DataSent"/> event.
    /// </summary>
    internal class DataSentEventArgs : DataEventArgs
    {
        public DataSentEventArgs(string data)
            : base(data) { }
    }

}
