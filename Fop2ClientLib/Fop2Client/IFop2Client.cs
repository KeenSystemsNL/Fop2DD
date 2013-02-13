using System;
using System.Collections.Generic;
using System.Net;
namespace Fop2ClientLib
{
    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.Disconnected"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    public delegate void DisconnectedEventHandler(object sender);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.Connected"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    public delegate void ConnectedEventHandler(object sender);

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
    /// Represents the method that will handle the <see cref="IFop2Client.AuthenticationFailed"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    public delegate void AuthenticationFailedEventHandler(object sender);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.AuthenticationSucceeded"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    public delegate void AuthenticationSucceededEventHandler(object sender);

    /// <summary>
    /// Represents the method that will handle the <see cref="IFop2Client.Heartbeat"/> event.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="HeartbeatEventArgs"/> containing information about the heartbeat "reply" (e.g. "pong") received.</param>
    public delegate void HeartbeatEventHandler(object sender, HeartbeatEventArgs e);

    interface IFop2Client
    {
        event AuthenticationFailedEventHandler AuthenticationFailed;
        event AuthenticationSucceededEventHandler AuthenticationSucceeded;
        event ConnectedEventHandler Connected;
        event DisconnectedEventHandler Disconnected;
        event HeartbeatEventHandler Heartbeat;
        event MessageReceivedEventHandler MessageReceived;
        event MessageSentEventHandler MessageSent;
        
        void Authenticate(string context, string username, string password);
        void Connect(string host, int port);
        void Connect(IPEndPoint ipendpoint);
        void Disconnect();
        void Send(params string[] args);
        void Send(string message);

        string Key { get; }
        string Context { get; }
        string Username { get; }
        string Password { get; }
        TimeSpan HeartbeatInterval { get; set; }
        Version HostVersion { get; }
        int LicenseLevel { get; }
        Dictionary<string, string> Preferences { get; }

        bool IsConnected { get; }
        bool IsAuthenticated { get; }
    }
}
