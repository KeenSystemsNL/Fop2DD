using System;
using System.Net;
using System.Text;

namespace Fop2ClientLib
{
    internal delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);
    internal delegate void DataSentEventHandler(object sender, DataSentEventArgs e);
    
    interface IAsyncClient
    {
        event ConnectionStateChangedEventHandler ConnectionStateChanged;
        event DataReceivedEventHandler DataReceived;
        event DataSentEventHandler DataSent;
        
        void Connect(IPEndPoint ipendpoint, TimeSpan timeout);
        void Disconnect();

        Encoding Encoding { get; }
        bool IsConnected { get; }
        void Send(string message);
    }
}
