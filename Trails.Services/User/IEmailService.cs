namespace Trails.Services.User
{
    public interface IEmailService
    {
        Task<bool> SendEmailPasswordReset(string to, string link);
    }
}
