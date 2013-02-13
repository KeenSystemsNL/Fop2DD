using System;

namespace Fop2ClientLib
{

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.MessageReceived"/> event.
    /// </summary>
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message that was received by a client.
        /// </summary>
        public Fop2Message Message { get; private set; }

        /// <summary>
        /// Initializes a new MessageReceivedEventArgs.
        /// </summary>
        /// <param name="message">Message that was received.</param>
        internal MessageReceivedEventArgs(Fop2Message message)
        {
            this.Message = message;
        }
    }

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.MessageSent"/> event.
    /// </summary>
    public class MessageSentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message that was sent by a client.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes a new MessageSentEventArgs.
        /// </summary>
        /// <param name="data">Data associated with the MessageSentEventArgs.</param>
        internal MessageSentEventArgs(string data)
        {
            this.Message = data;
        }
    }
}
