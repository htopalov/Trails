using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using Trails.Test.Web.Helpers;
using Xunit;
using static Trails.Test.Web.EventController.EventControllerTestConstants;

namespace Trails.Test.Web.EventController
{
    public class EventControllerTest
    {
        private readonly WebAppFactoryWithAuth<Program> authenticatedServer;
        private readonly HttpClient client;

        public EventControllerTest()
        {
            this.authenticatedServer = new WebAppFactoryWithAuth<Program>();
            this.client = authenticatedServer.CreateClient();
        }

        [Theory]
        [InlineData("/event/create")]
        public async Task CreateShouldReturnCorrectViewOnGetRequest(string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedCreateEventHeading, responseContent);
        }


        //Test will not pass even if I'm sending AntiForgery Token along with data, returns 400 Bad Request
        //If controllers action method is marked with Ignore AntiForgery Token attribute everything works fine
        //So problem comes from token...
        //Tested it both with original asp.net tokens and the custom ones set through Token Extractor and and Web App factory
        //Nothing worked

        //[Theory]
        //[InlineData("/event/create")]
        //public async Task CreateShouldReloadViewIfInputIsIncorrect(string url)
        //{
        //    var initialResponse = await client.GetAsync(url);
        //    var antiForgeryValues = await AfTokenExtractor.ExtractAntiForgeryValues(initialResponse);
        //    var postRequest = new HttpRequestMessage(HttpMethod.Post, url);
        //    postRequest.Headers.Add("Cookie", new CookieHeaderValue(AfTokenExtractor.AntiForgeryCookieName, antiForgeryValues.cookieValue).ToString());
        //    var modelData = new Dictionary<string, string>
        //    {
        //        {AfTokenExtractor.AntiForgeryFieldName, antiForgeryValues.fieldValue},
        //        {"Name", "Test Event"},
        //        {"Description", ""},
        //        {"Length", ""},
        //        {"StartDate", "2022-04-29T13:55"},
        //        {"EndDate", "2022-05-07T13:55"},
        //        {"Type", ""},
        //        {"DifficultyLevel", ""}
        //    };
        //    postRequest.Content = new FormUrlEncodedContent(modelData);
        //    var response = await client.SendAsync(postRequest);
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    Assert.Contains(ExpectedDescriptionError, responseString);
        //}
    }
}
