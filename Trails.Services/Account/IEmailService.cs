namespace Trails.Services.Account
{
    public interface IEmailService
    {
        Task<bool> SendEmailPasswordReset(string to, string link);
    }
}
