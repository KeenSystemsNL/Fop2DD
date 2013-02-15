using System;
using System.Linq;
using System.Net;

namespace Fop2DD.Core.Connection
{
    public class DDConnectionInfo
    {
        public DDFop2Endpoint Fop2Endpoint { get; private set; }
        public DDCredential Credential { get; private set; }
        public TimeSpan ConnectionTimeout { get; private set; }
        public TimeSpan PingInterval { get; private set; }

        public DDConnectionInfo(DDFop2Endpoint fop2endpoint, DDCredential credential, TimeSpan pinginterval)
            : this(fop2endpoint, credential, pinginterval, TimeSpan.FromSeconds(10)) { }

        public DDConnectionInfo(DDFop2Endpoint fop2endpoint, DDCredential credential, TimeSpan pinginterval, TimeSpan connectiontimeout)
        {
            this.Fop2Endpoint = fop2endpoint;
            this.Credential = credential;
            this.PingInterval = pinginterval;
            this.ConnectionTimeout = connectiontimeout;
        }

        public IPEndPoint GetIPEndPoint()
        {
            return new IPEndPoint(Dns.GetHostAddresses(this.Fop2Endpoint.Host).First(), this.Fop2Endpoint.Port);
        }
    }
}