using Trails.Models.Contact;
using Trails.Services.User;

namespace Trails.Test.UserServiceTests
{
    public static class UserServiceTestData
    {
        public const string To = "email@email.com";
        public const string Link = "reset link for password";

        public static EmailConfiguration GetTestConfiguration()
            => new()
            {
                From = "testSender@test.com",
                ContactTo = "testRecipient@test.com",
                Subject = "This is test",
                UserName = "test-username",
                ApiKey = "1234567890ASD"
            };

        public static ContactModel GetTestContactModel()
            => new()
            {
                Fullname = "Konstantin Georgiev",
                Email = "kgeorgiev@test.com",
                Message = "This is test message",
                PhoneNumber = "0887456901"
            };
    }
}
