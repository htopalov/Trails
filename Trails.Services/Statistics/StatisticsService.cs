using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Models.Statistics;

namespace Trails.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly TrailsDbContext dbContext;

        public StatisticsService(TrailsDbContext dbContext) 
            => this.dbContext = dbContext;

        public async Task<StatisticsModel> GetTrailsStatisticsAsync()
        {
            var totalEvents = await this.dbContext
                .Events
                .CountAsync();

            var totalRoutes = await this.dbContext
                .Routes
                .CountAsync();

            var totalUsers = await this.dbContext
                .Users
                .CountAsync();

            var totalParticipants = await this.dbContext
                .Participants
                .CountAsync();

            return new StatisticsModel
            {
                TotalEventsCount = totalEvents,
                TotalRoutesCount = totalRoutes,
                TotalUsersCount = totalUsers,
                TotalParticipantsCount = totalParticipants
            };
        }
    }
}
