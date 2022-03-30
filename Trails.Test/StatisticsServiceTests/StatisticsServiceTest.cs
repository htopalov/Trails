using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Trails.Data;
using Trails.Services.Statistics;
using static Trails.Test.StatisticsServiceTests.StatisticsServiceTestData;
using static Trails.Test.StatisticsServiceTests.StatisticsServiceConstants;

namespace Trails.Test.StatisticsServiceTests
{
    public class StatisticsServiceTest
    {
        [Test]
        public void GetTotalsShouldProduceCorrectResult()
        {
            var options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: "TrailsTestDb")
                .Options;

            using var context = new TrailsDbContext(options);
            context.Users.AddRange(GetTestUsers());
            context.Events.AddRange(GetTestEvents());
            context.Participants.AddRange(GetTestParticipants());
            context.Routes.AddRange(GetTestRoutes());
            context.SaveChanges();

            var service = new StatisticsService(context);

            Assert.AreEqual(ExpectedEventsCount, service.GetTrailsStatisticsAsync().Result.TotalEventsCount);
            Assert.AreEqual(ExpectedParticipantsCount, service.GetTrailsStatisticsAsync().Result.TotalParticipantsCount);
            Assert.AreEqual(ExpectedUsersCount, service.GetTrailsStatisticsAsync().Result.TotalUsersCount);
            Assert.AreEqual(ExpectedRoutesCount, service.GetTrailsStatisticsAsync().Result.TotalRoutesCount);
        }
    }
}
