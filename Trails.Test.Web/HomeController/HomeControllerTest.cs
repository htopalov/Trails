using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Trails.Test.Web.Helpers;
using Xunit;
using static Trails.Test.Web.HomeController.HomeControllerTestConstants;
using HttpMethod = System.Net.Http.HttpMethod;

namespace Trails.Test.Web.HomeController
{
    public class HomeControllerTest
    {
        private readonly WebAppFactoryWithoutAuth<Program> anonymousServer;
        private readonly WebAppFactoryWithAuth<Program> authenticatedServer;

        public HomeControllerTest()
        {
            this.anonymousServer = new WebAppFactoryWithoutAuth<Program>();
            this.authenticatedServer = new WebAppFactoryWithAuth<Program>();
        }

        [Theory]
        [InlineData("/")]
        public async Task HomeControllerShouldReturnAnonymousPartialViewWhenAnonymousUser(string url)
        {
            var client = anonymousServer.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHomeControllerParagraphFromAnonymousPartial, responseContent);
        }

        [Theory]
        [InlineData("/")]
        public async Task HomeControllerShouldReturnLoggedInPartialViewWhenLoggedUser(string url)
        {
            var client = authenticatedServer.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeaderWhenLogged, responseContent);
        }

        [Theory]
        [InlineData("/identity/account/login")]
        public async Task HomeControllerShouldReloadLoginViewWhenInputIncorrect(string url)
        {
            var client = anonymousServer.CreateClient();
            var initialResponse = await client.GetAsync(url);
            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                { "Email", "test@test.com"},
                {"Password", "123456"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedErrorMessageInvalidLogin, responseContent);
        }


        [Theory]
        [InlineData("/home/faq")]
        public async Task HomeControllerShouldReturnFaqPage(string url)
        {
            var client = anonymousServer.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeadingInFaqView, responseContent);
        }

        [Theory]
        [InlineData("/home/error")]
        public async Task HomeControllerShouldReturnErrorPage(string url)
        {
            var client = anonymousServer.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeadingInErrorView, responseContent);
        }

        [Theory]
        [InlineData("/home/contact")]
        public async Task HomeControllerShouldReturnContactPage(string url)
        {
            var client = anonymousServer.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeadingInContactPage, responseContent);
        }

        [Theory]
        [InlineData("/home/contact")]
        public async Task ContactPagePostRequestInFormShouldRedirectToIndexAfterCompletion(string url)
        {
            var client = anonymousServer.CreateClient();
            var initialResponse = await client.GetAsync(url);
            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Fullname", "Borislav Mihaylov"},
                { "Email", "bmihaylov@test.com"},
                {"PhoneNumber", "0885678901"},
                {"Message", "Test message from anonymous user."}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedMessageNotificationAfterContact, responseContent);
        }

        [Theory]
        [InlineData("/home/contact")]
        public async Task ContactPagePostRequestInFormShouldReloadIfValidationsDoNotPass(string url)
        {
            var client = anonymousServer.CreateClient();
            var initialResponse = await client.GetAsync(url);
            var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);
            var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
            var formModel = new Dictionary<string, string>
            {
                {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
                {"Fullname", ""},
                { "Email", "bmihaylov@test.com"},
                {"PhoneNumber", "0885678"},
                {"Message", "Test"}
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedErrorForMessage, responseContent);
            Assert.Contains(ExpectedErrorForFullname, responseContent);
            Assert.Contains(ExpectedErrorForPhoneFormat, responseContent);
        }
    }
}
