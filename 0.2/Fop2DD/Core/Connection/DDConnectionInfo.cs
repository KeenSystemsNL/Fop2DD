using Fop2ClientLib;
using GlobalHotKey;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Fop2DD.Core.Connection
{
    public class DDConnectionInfo
    {
        public IPEndPoint IPEndPoint { get; private set; }
        public DDCredential Credential { get; private set; }

        public DDConnectionInfo(IPEndPoint ipendpoint, DDCredential credential)
        {
            this.IPEndPoint = ipendpoint;
            this.Credential = credential;
        }
    }

 
}