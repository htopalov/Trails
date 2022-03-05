using Trails.Models.Statistics;

namespace Trails.Services.Statistics
{
    public interface IStatisticsService
    {
        Task<StatisticsModel> GetTrailsStatisticsAsync();
    }
}
