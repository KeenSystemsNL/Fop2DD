using System;
using System.IO.Pipes;
using System.Text;

namespace Fop2DD.Core.IPC
{
    class DDPipeClient
    {
        public void Send(string value, string pipeName, int timeout = 1000)
        {
            try
            {
                NamedPipeClientStream pipestream = new NamedPipeClientStream(".", pipeName, PipeDirection.Out, PipeOptions.Asynchronous);
                // The connect function will indefinitely wait for the pipe to become available
                // If that is not acceptable specify a maximum waiting time (in ms)
                pipestream.Connect(timeout);
                byte[] _buffer = Encoding.UTF8.GetBytes(value);
                pipestream.BeginWrite(_buffer, 0, _buffer.Length, new AsyncCallback(AsyncSend), pipestream);
            }
            catch
            {

            }
        }

        private void AsyncSend(IAsyncResult iar)
        {
            try
            {
                // Get the pipe
                NamedPipeClientStream pipeStream = (NamedPipeClientStream)iar.AsyncState;

                // End the write
                pipeStream.EndWrite(iar);
                pipeStream.Flush();
                pipeStream.Close();
                pipeStream.Dispose();
            }
            catch
            {

            }
        }
    }
}
