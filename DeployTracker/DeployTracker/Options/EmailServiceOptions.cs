using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployTracker.Options
{
    public class EmailServiceOptions
    {  
        public string MailFrom { get; set; }
        public string MailFromPassword { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }

    }
}
