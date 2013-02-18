using Fop2ClientLib;
using System;
using System.Collections.Generic;

namespace Fop2DD.Core.Connection
{
    public class DDConnectionManager : IDDConnectionStateChangeNotifyable
    {
        private bool _isauthenticated;
        private IFop2Client _client;
        private DDConnectionInfo _connectioninfo;

        private List<IDDConnectionStateChangeNotifyable> _registeredobjects;

        public DDConnectionManager(IFop2Client client)
        {
            _isauthenticated = false;
            _client = client;

            _registeredobjects = new List<IDDConnectionStateChangeNotifyable>();

            _client.AuthenticationResultReceived += (s, e) => { this.StateChanged(s, new DDConnectionStateChangedEventArgs(e.Result == AuthenticationStatus.Success ? DDConnectionState.AuthenticationSucceeded : DDConnectionState.AuthenticationFailed)); };
            _client.ConnectionError += (s, e) => { this.StateChanged(s, new DDConnectionStateChangedEventArgs(e.Exception.GetType().Equals(typeof(TimeoutException)) ? DDConnectionState.ConnectionTimedOut : DDConnectionState.ConnectionLost, e.Exception.Message)); };
            _client.ConnectionStateChanged += (s, e) => { this.StateChanged(s, new DDConnectionStateChangedEventArgs(e.ConnectionState == ConnectionState.Connected ? DDConnectionState.Connected : DDConnectionState.ConnectionLost)); };
        }

        public void Connect(DDConnectionInfo connectioninfo)
        {
            if (connectioninfo == null)
                throw new ArgumentNullException("connectioninfo");

            _connectioninfo = connectioninfo;
            _client.Connect(_connectioninfo.GetIPEndPoint(), _connectioninfo.ConnectionTimeout);
            _client.HeartbeatInterval = _connectioninfo.PingInterval;
        }

        public void Disconnect()
        {
            _isauthenticated = false;   //Force authenticated to false to prevent reconnecting on the Disconnected event
            _client.Disconnect();
        }

        public void StateChanged(object sender, DDConnectionStateChangedEventArgs e)
        {
            _isauthenticated = false;
            switch (e.State)
            {
                case DDConnectionState.Connected:
                    _client.Authenticate(_connectioninfo.Credential.Context, _connectioninfo.Credential.Username, _connectioninfo.Credential.Password);
                    break;
                case DDConnectionState.AuthenticationSucceeded:
                    _isauthenticated = true;
                    break;
                case DDConnectionState.ConnectionLost:
                    if (_isauthenticated)   //Retry dropped connections if we were authenticated
                        this.Connect(_connectioninfo);
                    break;
            }

            foreach (var o in _registeredobjects)
                o.StateChanged(sender, e);
        }

        public void RegisterListener(IDDConnectionStateChangeNotifyable listener)
        {
            _registeredobjects.Add(listener);
        }

        public void UnregisterListener(IDDConnectionStateChangeNotifyable listener)
        {
            _registeredobjects.Remove(listener);
        }
    }
}
