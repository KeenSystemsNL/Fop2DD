using System;

namespace Fop2ClientLib
{
    /// <summary>
    /// Base class for MessageReceived/Sent events
    /// </summary>
    public abstract class MessageEventArgs : EventArgs
    {
    }

    /// <summary>
    /// Provides data for the <see cref="IFop2Client.MessageReceived"/> event.
    /// </summary>
    public class MessageReceivedEventArgs : MessageEventArgs
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
    public class MessageSentEventArgs : MessageEventArgs
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
