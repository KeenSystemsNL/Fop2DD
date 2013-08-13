using Fop2ClientLib;
using Fop2DD.Core.Logging;
using System;
using System.Collections.Generic;

namespace Fop2DD.Core.Connection
{
    public class DDConnectionManager : IDDConnectionStateChangeNotifyable
    {
        private bool _isauthenticated;
        private bool _reconnect;
        private IFop2Client _client;
        private DDConnectionInfo _connectioninfo;

        private List<IDDConnectionStateChangeNotifyable> _registeredobjects;

        private IDDLogger logger = DDLogManager.GetLogger(typeof(DDConnectionManager));

        public DDConnectionManager(IFop2Client client)
        {
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


            logger.LogInfo("Connecting to {0}:{1}", connectioninfo.Fop2Endpoint.Host, connectioninfo.Fop2Endpoint.Port);

            _isauthenticated = false;
            _connectioninfo = connectioninfo;
            _client.Connect(_connectioninfo.GetIPEndPoint(), _connectioninfo.ConnectionTimeout);
            _client.HeartbeatInterval = _connectioninfo.PingInterval;
        }

        public void Reconnect(DDConnectionInfo connectioninfo)
        {
            logger.LogInfo("Reconnecting");

            _connectioninfo = connectioninfo;
            _reconnect = true;
            this.Disconnect();
        }

        public void Disconnect()
        {
            logger.LogInfo("Disconnecting");

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
                case DDConnectionState.ConnectionTimedOut:
                case DDConnectionState.ConnectionLost:
                    if (_isauthenticated || _reconnect)   //Retry dropped/failed connections if we were authenticated or when reconnect is used
                        this.Connect(_connectioninfo);
                    _reconnect = false;
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
