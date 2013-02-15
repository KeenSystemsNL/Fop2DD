using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fop2DD.Core.Connection
{
    public class DDCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Context { get; set; }

        public DDCredential(string username, string password)
            : this(string.Empty, username, password) { }

        public DDCredential(string context, string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Context = context;
        }
    }
}
