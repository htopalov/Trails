using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Trails.Data;
using Trails.Infrastructure;
using Trails.Services.Administration;
using static Trails.Test.AdministrationServiceTests.AdministrationServiceTestData;
using static Trails.Test.AdministrationServiceTests.AdministrationServiceConstants;

namespace Trails.Test.AdministrationServiceTests
{
    public class AdministrationServiceTest
    {
        private DbContextOptions<TrailsDbContext> options;
        private TrailsDbContext context;
        private AdministrationService adminService;
        private IMapper mapper;

        [SetUp]
        public async Task Setup()
        {
            options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TrailsDbContext(options);
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))));
            adminService = new AdministrationService(context, mapper);
            await context.Users.AddAsync(GetTestUser());
            await context.Events.AddRangeAsync(GetTestEvents());
            await context.Routes.AddRangeAsync(GetTestRoutes());
            await context.Participants.AddRangeAsync(GetTestParticipants());
            await context.Beacons.AddRangeAsync(GetTestBeacons());
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task GetUnapprovedEventsCountShouldReturnCorrectResult()
        {
            var result = await adminService.GetUnapprovedEventsCountAsync();
            Assert.AreEqual(ExpectedUnapprovedEventsCount, result);
        }

        [Test]
        public async Task GetAllUnapprovedEventsShouldReturnOnlyNotDeletedNotApprovedAndWithRoute()
        {
            var result = await adminService.GetAllUnapprovedEventsAsync();
            Assert.AreEqual(ExpectedUnapprovedEventsCount, result.Events.Count);
        }

        [Test]
        public async Task GetAllUnapprovedEventsShouldReturnCorrectCountOfPagedEventsWhenNoEventsForCurrentPage()
        {
            var result = await adminService.GetAllUnapprovedEventsAsync(PageNumberWithLessEvents);
            Assert.AreEqual(ExpectedPagedEventsCount, result.Events.Count);
            Assert.AreEqual(PageNumberWithLessEvents, result.CurrentPage);
        }

        [Test]
        public async Task ApproveEventShouldReturnTrueIfEventExists()
        {
            var result = await adminService.ApproveEventAsync(EventToBeApprovedId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApproveEventShouldReturnFalseWhenEventDoesNotExist()
        {
            var result = await adminService.ApproveEventAsync(EventWithInvalidId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApproveEventShouldReturnTrueWhenExistsAsUnApprovedAndNotDeletedBeforeApproveFromAdmin()
        {
            var result = await adminService.ApproveEventAsync(EventToBeApprovedAndNotDeletedId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApproveEventShouldReturnFalseWhenExistingAsApprovedAndDeleted()
        {
            var result = await adminService.ApproveEventAsync(EventApprovedAndDeletedId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeclineEventShouldReturnTrueIfEventForApproveExists()
        {
            var result = await adminService.DeclineEventAsync(EventToDeclineId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeclineEventShouldReturnFalseIfEventDoesNotExist()
        {
            var result = await adminService.DeclineEventAsync(EventWithInvalidId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DetachBeaconsFromParticipantsShouldReturnTrueIfNoExceptionIsRaised()
        {
            var result = await adminService.DetachBeaconsFromParticipantsInPassedEventsAsync();
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetEventsToPrepareShouldReturnResultWhenAny()
        {
            var result = await adminService.GetEventsToPrepareAsync();
            Assert.AreEqual(EventsToPrepareCount, result.Count);
        }

        [Test]
        public async Task GetParticipantsToPrepareShouldReturnNullIfEventIdDoesNotExist()
        {
            var result = await adminService.GetParticipantsToPrepareAsync(EventWithInvalidId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetParticipantsToPrepareShouldReturnParticipantsListIfApprovedAndBeaconIsNull()
        {
            var result = await adminService.GetParticipantsToPrepareAsync(EventToPrepareId);
            Assert.AreEqual(ExpectedCountOfParticipantsToPrepare, result.Count);
        }

        [Test]
        public async Task GetBeaconsToConnectShouldReturnOnlyBeaconsWhichAreNotConnected()
        {
            var result = await adminService.GetBeaconsToConnectAsync();
            Assert.AreEqual(ExpectedCountOfBeaconsAvailableToConnect, result.Count);
        }

        [Test]
        public async Task ConnectBeaconToParticipantShouldReturnFalseIfParticipantDoesNotExist()
        {
            var result = await adminService.ConnectBeaconToParticipantAsync(GetTestParticipantBeaconModelWithInvalidParticipant());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ConnectBeaconToParticipantShouldReturnFalseIfParticipantAlreadyHasBeaconConnected()
        {
            var result = await adminService.ConnectBeaconToParticipantAsync(GetTestParticipantBeaconModelWithValidBeacon());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ConnectBeaconToParticipantShouldReturnFalseIfBeaconDoesNotExist()
        {
            var result = await adminService.ConnectBeaconToParticipantAsync(GetTestParticipantBeaconModelWithInvalidBeacon());
            Assert.IsFalse(result);
        }
    }
}
