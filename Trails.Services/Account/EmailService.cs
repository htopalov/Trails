using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Trails.Services.Account
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config) 
            => this.config = config;

        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            var emailConfig = this.config
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfig.From);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(emailConfig.UserName, emailConfig.Password);
            //client.EnableSsl = true;
            client.Host = emailConfig.SmtpServer;
            client.Port = emailConfig.Port;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log
            }
            return false;
        }
    }
}
