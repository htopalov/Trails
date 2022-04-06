using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Trails.Test.Web.Helpers;
using Xunit;
using static Trails.Test.Web.UserController.UserControllerTestConstants;

namespace Trails.Test.Web.UserController
{
    public class UserControllerTest : IClassFixture<WebAppFactoryWithoutAuth<Program>>
    {
        private readonly WebAppFactoryWithoutAuth<Program> server;

        public UserControllerTest(WebAppFactoryWithoutAuth<Program> server)
            => this.server = server;

        [Theory]
        [InlineData("/user/forgotpassword")]
        public async Task ForgotPasswordShouldRedirectToPasswordConfirmation(string url)
        {
            var client = server.CreateClient();
            var initialResponse = await client.GetAsync(url);

            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Email", "test@test.com"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeadingMessage, responseContent);
        }

        [Theory]
        [InlineData("/user/forgotpassword")]
        public async Task ForgotPasswordShouldReloadViewWithMessageWhenEmailFormatIsInvalid(string url)
        {
            var client = server.CreateClient();
            var initialResponse = await client.GetAsync(url);

            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Email", "test12345"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedErrorNotificationMessage, responseContent);
        }

        [Theory]
        [InlineData("/user/forgotpasswordconfirmation")]
        public async Task ForgotPasswordConfirmationShouldReturnViewOnGetRequest(string url)
        {
            var client = server.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeadingMessage, responseContent);
        }

        [Theory]
        [InlineData("/user/resetpassword")]
        public async Task ResetPasswordShouldReturnViewOnGetRequest(string url)
        {
            var client = server.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeaderForPasswordReset, responseContent);
        }

        [Theory]
        [InlineData("/user/resetpassword")]
        public async Task ResetPasswordShouldRedirectToIndexOnPostRequest(string url)
        {
            var client = server.CreateClient();
            var initialResponse = await client.GetAsync(url);

            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Password", "test12345"},
                {"ConfirmPassword", "test12345"},
                {"Email", "test@test.com"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedSuccessNotificationMessage, responseContent);
        }

        [Theory]
        [InlineData("/user/resetpassword")]
        public async Task ResetPasswordShouldReloadViewWhenDataIsIncorrectOnPostRequest(string url)
        {
            var client = server.CreateClient();
            var initialResponse = await client.GetAsync(url);

            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);

            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Password", "test12345"},
                {"ConfirmPassword", "test5"},
                {"Email", "test@test.com"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedValidationError, responseContent);
        }
    }
}
