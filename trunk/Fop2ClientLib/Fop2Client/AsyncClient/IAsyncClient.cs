using System;
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
        
        void Connect(System.Net.IPEndPoint ipendpoint);
        void Disconnect();

        Encoding Encoding { get; }
        bool IsConnected { get; }
        void Send(string message);
    }
}
