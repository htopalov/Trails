using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Services.Statistics;

namespace Trails.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService) 
            => this.statisticsService = statisticsService;

        [AllowAnonymous]
        public async Task<IActionResult> TotalStatistics()
        {
            var result = await this.statisticsService
                .GetTrailsStatisticsAsync();

            return View(result);
        }
    }
}
