using SendGrid;
using SendGrid.Helpers.Mail;
using Trails.Models.Contact;

namespace Trails.Services.User
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

        public SendGridMessage PasswordResetMessage(string to, string link)
        {
            var fromAddress = new EmailAddress(config.From, config.UserName);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, config.Subject, link, null);
            return message;
        }

        public SendGridMessage ContactMessage(ContactModel contactModel)
        {
            var fromAddress = new EmailAddress(config.From, contactModel.Fullname);
            var toAddress = new EmailAddress(config.ContactTo);
            var message = MailHelper.CreateSingleEmail(
                fromAddress,
                toAddress,
                "Question from contact form",
                $"Sender Email: {contactModel.Email} {Environment.NewLine} {contactModel.Message} {Environment.NewLine} PhoneNumber: {contactModel.PhoneNumber}",
                null);

            return message;
        }

        public async Task<bool> SendEmailAsync(SendGridMessage message)
        {
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
