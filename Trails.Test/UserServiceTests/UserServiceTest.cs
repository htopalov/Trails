using NUnit.Framework;
using Trails.Services.User;
using static Trails.Test.UserServiceTests.UserServiceTestData;
using static Trails.Test.UserServiceTests.UserServiceTestConstants;
using System.Threading.Tasks;

namespace Trails.Test.UserServiceTests
{
    public class UserServiceTest
    {
        private IEmailService emailService;

        [SetUp]
        public void Setup() 
            => emailService = new EmailService(GetTestConfiguration());

        [Test]
        public void PasswordResetMessageShouldConstructCorrectMessageWhenDataIsValid()
        {
            var result = emailService.PasswordResetMessage(To, Link);
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordResetMessageShouldContainCorrectInformation()
        {
            var result = emailService.PasswordResetMessage(To, Link);
            Assert.AreEqual(ExpectedFromPropertyContent, result.From.Email);
            Assert.AreEqual(ExpectedMessageSubject, result.Subject);
        }

        [Test]
        public void ContactMessageShouldConstructCorrectMessageWhenDataIsValid()
        {
            var result = emailService.ContactMessage(GetTestContactModel());
            Assert.IsNotNull(result);
        }


        [Test]
        public void ContactMessageShouldContainCorrectInformation()
        {
            var result = emailService.ContactMessage(GetTestContactModel());
            Assert.AreEqual(ExpectedFullNameOfSender, result.From.Name);
        }

        [Test]
        public async Task SendMailShouldReturnTrueWithUnAuthorizedStatusCodeWhenNotConnectedToActualApiWithoutExceptionThrown()
        {
            var message = emailService.ContactMessage(GetTestContactModel());
            var result = await emailService.SendEmailAsync(message);
            Assert.IsTrue(result);
        }
    }
}
