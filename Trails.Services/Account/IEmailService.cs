namespace Trails.Services.Account
{
    public interface IEmailService
    {
        bool SendEmailPasswordReset(string userEmail, string link);
    }
}
