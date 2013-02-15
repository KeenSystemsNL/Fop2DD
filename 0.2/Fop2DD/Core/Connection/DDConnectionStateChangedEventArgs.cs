using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fop2DD.Core.Connection
{
    public class DDConnectionStateChangedEventArgs : EventArgs
    {
        public DDConnectionState State { get; private set; }
        public string Data { get; private set; }

        public DDConnectionStateChangedEventArgs(DDConnectionState state)
            : this(state, null) { }

        public DDConnectionStateChangedEventArgs(DDConnectionState state, string data)
        {
            this.State = state;
            this.Data = data;
        }
    }
}
