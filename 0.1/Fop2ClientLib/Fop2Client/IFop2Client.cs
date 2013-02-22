using System;
using System.Collections.Generic;
using System.Net;
namespace Fop2ClientLib
{
    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.ConnectionStateChanged"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ConnectionStateChangedEventArgs"/> containing information about the connectionstate change.</param>
    public delegate void ConnectionStateChangedEventHandler(object sender, ConnectionStateChangedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.MessageSent"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="MessageSentEventArgs"/> containing information about the message sent.</param>
    public delegate void MessageSentEventHandler(object sender, MessageSentEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.MessageReceived"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="MessageReceivedEventArgs"/> containing information about the message received.</param>
    public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.AuthenticationResultReceived"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="AuthenticationResultReceivedEventArgs"/> containing information about the received authentication status.</param>
    public delegate void AuthenticationResultReceivedEventHandler(object sender, AuthenticationResultReceivedEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.Heartbeat"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="HeartbeatEventArgs"/> containing information about the heartbeat "reply" (e.g. "pong") received.</param>
    public delegate void HeartbeatEventHandler(object sender, HeartbeatEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.ConnectionError"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ConnectionErrorEventArgs"/> containing information about the error.</param>
    public delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);

    /// <summary>
    /// Provides the base interface for implementation of a Fop2Client class.
    /// </summary>
    public interface IFop2Client
    {
        /// <summary>
        /// Occurs when the client received and authenticaiton result.
        /// </summary>
        event AuthenticationResultReceivedEventHandler AuthenticationResultReceived;

        /// <summary>
        /// Occurs when the clients connection state changes.
        /// </summary>
        event ConnectionStateChangedEventHandler ConnectionStateChanged;

        /// <summary>
        /// Occurs at the specified interval (<see cref="HeartbeatInterval"/>).
        /// </summary>
        event HeartbeatEventHandler Heartbeat;

        /// <summary>
        /// Occurs when the client received a message from the host.
        /// </summary>
        event MessageReceivedEventHandler MessageReceived;

        /// <summary>
        /// Occurs when the client has sent a message to the host.
        /// </summary>
        event MessageSentEventHandler MessageSent;

        /// <summary>
        /// Occurs when the client successfully authenticated.
        /// </summary>
        event ConnectionErrorEventHandler ConnectionError;

        /// <summary>
        /// Authenticates for a session.
        /// </summary>
        /// <param name="context">The context to use for the session.</param>
        /// <param name="username">The username to use for the session.</param>
        /// <param name="password">The password to use when authenticating.</param>
        void Authenticate(string context, string username, string password);

        /// <summary>
        /// Connects to the specified host.
        /// </summary>
        /// <param name="ipendpoint">The IpEndpoint to connect to.</param>
        /// <param name="timeout">Specifies the timespan after wich a timeout exception will be thrown during connecting to the other host.</param>
        void Connect(IPEndPoint ipendpoint, TimeSpan timeout);

        /// <summary>
        /// Disconnects from host.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Sends a message to the host; the message is wrapped in an "msg" Xml-element's "data" attribute.
        /// </summary>
        /// <param name="message">The message to send.</param>
        void Send(string message);

        /// <summary>
        /// Gets the "session" key.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the current context which is set during authentication.
        /// </summary>
        string Context { get; }

        /// <summary>
        /// Gets the current user which is set during authentication.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the current user which is set during authentication.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the "Id" of the useragent (also "position", "button number")
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets/sets the interval at which "heartbeat" / "keepalive" messages will be sent (e.g. ping).
        /// </summary>
        TimeSpan HeartbeatInterval { get; set; }

        /// <summary>
        /// Gets whether the client is connected to the host.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets whether the client is authenticated at the host.
        /// </summary>
        bool IsAuthenticated { get; }
    }
}
