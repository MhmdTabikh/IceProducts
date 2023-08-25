namespace IceProducts.Server.Emailing
{
    public class EmailConfiguration
    {
        public string Sender { get; set; }
        public string AdminEmail { get; set; }
        public string Password { get; set; }
        public bool SslEnabled { get; set; }
        public int Port { get; set; }
        public string SmtpServer { get; set; }
    }
}
