using System;
using System.IO.Pipes;
using System.Text;

namespace Fop2DD.Core.IPC
{
    public class DDPipeServer
    {
        public event MessageReceivedHandler MessageReceived;
        public delegate void MessageReceivedHandler(object sender, DDPipeMessageReceivedEventArgs e);

        private string _pipename;
        private const int BUFFERSIZE = 4096;

        public void Listen(string PipeName)
        {
            try
            {
                _pipename = PipeName;
                // Create the new async pipe 
                NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, BUFFERSIZE, 0);

                // Wait for a connection
                pipeServer.BeginWaitForConnection
                (new AsyncCallback(WaitForConnectionCallBack), pipeServer);
            }
            catch
            {

            }
        }

        private void WaitForConnectionCallBack(IAsyncResult iar)
        {
            try
            {
                // Get the pipe
                NamedPipeServerStream pipeserver = (NamedPipeServerStream)iar.AsyncState;
                // End waiting for the connection
                pipeserver.EndWaitForConnection(iar);

                byte[] buffer = new byte[BUFFERSIZE];

                // Read the incoming message
                var bytesread = pipeserver.Read(buffer, 0, buffer.Length);

                // Convert byte buffer to string
                string data = Encoding.UTF8.GetString(buffer, 0, bytesread);

                // Pass message back to calling form
                if (!string.IsNullOrEmpty(data) && (this.MessageReceived != null))
                    MessageReceived(this, new DDPipeMessageReceivedEventArgs(data));

                // Kill original sever and create new wait server
                pipeserver.Close();
                pipeserver.Dispose();
                pipeserver = new NamedPipeServerStream(_pipename, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, BUFFERSIZE, BUFFERSIZE);

                // Recursively wait for the connection again and again....
                pipeserver.BeginWaitForConnection(new AsyncCallback(WaitForConnectionCallBack), pipeserver);
            }
            catch
            {
                return;
            }
        }
    }


}
