
namespace Fop2DD.Core.Connection
{
    public class DDFop2Endpoint
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public DDFop2Endpoint(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }
    }
}
