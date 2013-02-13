using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Fop2ClientLib
{
    /// <summary>
    /// Internal simple wrapper around a <see cref="TcpClient"/> to allow asynchronous, event-based communication
    /// </summary>
    internal class AsyncClient : Fop2ClientLib.IAsyncClient
    {
        private class StateObject
        {
            public byte[] Buffer { get; set; }

            public StateObject(byte[] buffer)
            {
                this.Buffer = buffer;
            }
        }

        private TcpClient _client;
        private int _buffersize;

        private AsyncOperation _operation;

        public event DataReceivedEventHandler DataReceived;

        public event DataSentEventHandler DataSent;

        public event DisconnectedEventHandler Disconnected;

        public event ConnectedEventHandler Connected;

        public Encoding Encoding { get; private set; }

        public AsyncClient()
            : this(Encoding.UTF8) { }

        public AsyncClient(Encoding encoding)
            : this(encoding, 1024) { }

        public AsyncClient(Encoding encoding, int buffersize)
        {
            this.Encoding = encoding;
            _buffersize = buffersize;
        }

        public bool IsConnected
        {
            get
            {
                if (_client == null)
                    return false;
                return _client.Connected;
            }
        }

        public void Connect(IPEndPoint ipendpoint)
        {
            _operation = AsyncOperationManager.CreateOperation(null);

            _client = new TcpClient(ipendpoint.AddressFamily);
            _client.NoDelay = true;

            _client.BeginConnect(ipendpoint.Address, ipendpoint.Port, new AsyncCallback(EndConnect), new StateObject(null));
        }

        public void Disconnect()
        {
            if (_client != null)
            {
                try
                {
                    _client.Close();
                    _client = null;
                }
                catch { }

                RaiseEvent(() => { if (this.Disconnected != null) Disconnected(this); });
            }
        }

        private void EndConnect(IAsyncResult result)
        {
            var so = result.AsyncState as StateObject;
            NetworkStream networkStream;

            try
            {
                _client.EndConnect(result);
                networkStream = _client.GetStream();

                RaiseEvent(() => { if (this.Connected != null) Connected(this); });
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return;
            }

            try
            {
                so.Buffer = new byte[_buffersize];
                networkStream.BeginRead(so.Buffer, 0, so.Buffer.Length, this.EndRead, new StateObject(so.Buffer));
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        public void Send(string message)
        {
            if ((_client != null) && (_client.Connected))
            {
                try
                {
                    var networkStream = _client.GetStream();
                    var data = this.Encoding.GetBytes(message);
                    networkStream.BeginWrite(data, 0, data.Length, this.EndWrite, new StateObject(data));
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }

        private void EndWrite(IAsyncResult result)
        {
            var so = result.AsyncState as StateObject;
            try
            {
                var networkStream = _client.GetStream();
                networkStream.EndWrite(result);

                RaiseEvent(() => { if (this.DataSent != null) DataSent(this, new DataSentEventArgs(this.Encoding.GetString(so.Buffer))); });
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void EndRead(IAsyncResult result)
        {
            var so = result.AsyncState as StateObject;
            try
            {
                var networkStream = _client.GetStream();
                int bytesread = networkStream.EndRead(result);

                if (bytesread > 0)
                {
                    string data = this.Encoding.GetString(so.Buffer, 0, bytesread);
                    RaiseEvent(() => { if (this.DataReceived != null) DataReceived(this, new DataReceivedEventArgs(data)); });

                    networkStream.BeginRead(so.Buffer, 0, so.Buffer.Length, EndRead, new StateObject(so.Buffer));
                }
                else
                {
                    this.Disconnect();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            if (this.IsConnected)
                this.Disconnect();
        }

        private void RaiseEvent(Action method)
        {
            _operation.Post(new SendOrPostCallback(delegate(object obj) { method.Invoke(); }), null);
        }
    }

}
