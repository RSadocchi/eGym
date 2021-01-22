using System;
using System.Collections.Generic;
using System.Text;

namespace eGym.Application.Option
{
    public class EmailOptions : IEmailServerOptions
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string DefaultBcc { get; set; }

        public string LocalDomain { get; set; }

        public string MailServerAddress { get; set; }
        public int MailServerPort { get; set; }
        public bool UseSSL { get; set; }
        public bool UseTLS { get; set; }

        public string UserId { get; set; }
        public string UserPassword { get; set; }

        public bool Debug { get; set; }
        public string DebugDefaultTo { get; set; }
        public string DebugAllowedAddresses { get; set; }
        public string ErrorTo { get; set; }
        public string BatchResultTo { get; set; }
    }
}
