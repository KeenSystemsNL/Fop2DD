using System;

namespace Fop2DD.Core.IPC
{
    public class DDPipeMessageReceivedEventArgs : EventArgs
    {
        public string Data { get; set; }

        public DDPipeMessageReceivedEventArgs(string data)
        {
            this.Data = data;
        }
    }
}
