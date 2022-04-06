using System.Threading.Tasks;
using Trails.Test.Web.Helpers;
using Xunit;

namespace Trails.Test.Web.StatisticsController
{
    public class StatisticsControllerTest
    {
        private readonly string ExpectedHeading = "Trails application statistics";
        private readonly WebAppFactoryWithoutAuth<Program> server;

        public StatisticsControllerTest()
            => this.server = new WebAppFactoryWithoutAuth<Program>();

        [Theory]
        [InlineData("/statistics/totalstatistics")]
        public async Task StatisticsControllerShouldReturnCorrectView(string url)
        {
            var client = server.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains(ExpectedHeading, responseContent);
        }
    }
}
