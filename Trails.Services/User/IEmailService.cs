using SendGrid.Helpers.Mail;
using Trails.Models.Contact;

namespace Trails.Services.User
{
    public interface IEmailService
    {
        SendGridMessage PasswordResetMessage(string to, string link);

        SendGridMessage ContactMessage(ContactModel contactModel);

        Task<bool> SendEmailAsync(SendGridMessage message);
    }
}
