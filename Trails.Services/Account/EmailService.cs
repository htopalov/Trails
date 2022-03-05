using SendGrid;
using SendGrid.Helpers.Mail;

namespace Trails.Services.Account
{
    public class EmailService : IEmailService
    {
        private readonly SendGridClient client;
        private readonly EmailConfiguration config;

        public EmailService(EmailConfiguration config)
        {
            this.config = config;
            this.client = new SendGridClient(config.ApiKey);
        }

        public async Task<bool> SendEmailPasswordReset(string to, string link)
        {
            var fromAddress = new EmailAddress(config.From, config.UserName);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, config.Subject, link, null);

            try
            {
                var response = await this.client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Body.ReadAsStringAsync());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
