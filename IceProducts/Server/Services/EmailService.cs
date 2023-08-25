using IceProducts.Server.Emailing;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using MimeKit;
using MailKit.Net.Smtp;

namespace IceProducts.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;

        public EmailService(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(ContactUsInputModel contactUsInput)
        {
            MimeMessage message = CreateMessage(contactUsInput.Subject, contactUsInput.EmailAddress, contactUsInput.Name, contactUsInput.Message);

            // email sender (not the user email, it's the email that the user will send from)
            message.From.Add(MailboxAddress.Parse(_configuration.Sender));
            message.To.Add(MailboxAddress.Parse(_configuration.AdminEmail));

            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_configuration.SmtpServer, _configuration.Port, _configuration.SslEnabled);
                    smtp.AuthenticationMechanisms.Remove("XOAUTH2");
                    await smtp.AuthenticateAsync(_configuration.Sender, _configuration.Password);
                    await smtp.SendAsync(message);
                }
                catch
                {
                    throw;
                }

                finally
                {
                    await smtp.DisconnectAsync(true);
                    smtp.Dispose();
                }
            }
        }

        private MimeMessage CreateMessage(string subject, string emailContact, string nameContact, string body)
        {
            var message = new MimeMessage();
            message.Subject = subject;
            message.Body = new BodyBuilder
            {
                HtmlBody = GetMessageTemplate(nameContact, emailContact, body)
            }.ToMessageBody();

            return message;
        }

        private string GetMessageTemplate(string nameContact, string emailContact, string body)
        {
            string pStart = "<p>"; string pEnd = "</p>";
            string header = $"You're receiving an email from :{nameContact}\n email: {emailContact}\n";
            string messageBody = $"Message content:\n {body}";

            return pStart + header + messageBody + pEnd;
        }
    }
}
