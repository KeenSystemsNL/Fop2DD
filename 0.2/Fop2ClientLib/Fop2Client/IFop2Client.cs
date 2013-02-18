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

    public interface IFop2Client
    {
        event AuthenticationResultReceivedEventHandler AuthenticationResultReceived;
        event ConnectionStateChangedEventHandler ConnectionStateChanged;
        event HeartbeatEventHandler Heartbeat;
        event MessageReceivedEventHandler MessageReceived;
        event MessageSentEventHandler MessageSent;
        event ConnectionErrorEventHandler ConnectionError;
        
        void Authenticate(string context, string username, string password);
        void Connect(string host, int port);
        void Connect(IPEndPoint ipendpoint, TimeSpan timeout);
        void Disconnect();
        void Send(params string[] args);
        void Send(string message);

        string Key { get; }
        string Context { get; }
        string Username { get; }
        string Password { get; }
        int Id { get; }
        TimeSpan HeartbeatInterval { get; set; }
        Version HostVersion { get; }
        int LicenseLevel { get; }
        Dictionary<string, string> Preferences { get; }

        bool IsConnected { get; }
        bool IsAuthenticated { get; }
    }
}
