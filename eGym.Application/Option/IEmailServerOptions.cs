namespace eGym.Application.Option
{
    public interface IEmailServerOptions
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }

        public string MailServerAddress { get; set; }
        public int MailServerPort { get; set; }
        public bool UseSSL { get; set; }
        public bool UseTLS { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}
