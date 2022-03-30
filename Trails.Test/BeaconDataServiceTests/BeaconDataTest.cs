using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Trails.Api.Services;
using Trails.Data;
using Trails.Infrastructure;
using static Trails.Test.BeaconDataServiceTests.BeaconDataTestData;

namespace Trails.Test.BeaconDataServiceTests
{
    public class BeaconDataTest
    {
        private DbContextOptions<TrailsDbContext> options;
        private TrailsDbContext context;
        private IBeaconDataService beaconDataService;
        private IMapper mapper;

        [SetUp]
        public async Task Setup()
        {
            options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TrailsDbContext(options);
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))));
            beaconDataService = new BeaconDataService(context, mapper);
            await context.Events.AddRangeAsync(GetTestEvents());
            await context.Beacons.AddRangeAsync(GetTestBeacons());
            await context.Participants.AddRangeAsync(GetTestParticipants());
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task CreateBeaconDataShouldReturnNullIfParticipantWithIncorrectBeaconImeiDoesNotExist()
        {
            var result = await beaconDataService.CreateBeaconDataAsync(DataWithIncorrectBeaconImei());
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateBeaconDataShouldReturnBroadcastModel()
        {
            var result = await beaconDataService.CreateBeaconDataAsync(DataWithCorrectBeaconImei());
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateBeaconDataShouldReturnNullIfTimestampIsBeforeParticipantsEventStart()
        {
            var result = await beaconDataService.CreateBeaconDataAsync(DataWithCorrectBeaconImeiForFutureEvent());
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateBeaconDataShouldReturnNullIfTimestampIsAfterParticipantsEventEnd()
        {
            var result = await beaconDataService.CreateBeaconDataAsync(DataWithCorrectBeaconImeiForPastEvent());
            Assert.IsNull(result);
        }
    }
}
