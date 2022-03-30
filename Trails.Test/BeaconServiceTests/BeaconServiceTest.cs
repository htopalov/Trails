using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Trails.Data;
using Trails.Infrastructure;
using Trails.Services.Beacon;
using static Trails.Test.BeaconServiceTests.BeaconServiceTestData;
using static Trails.Test.BeaconServiceTests.BeaconServiceTestConstants;

namespace Trails.Test.BeaconServiceTests
{
    public class BeaconServiceTest
    {
        private DbContextOptions<TrailsDbContext> options;
        private TrailsDbContext context;
        private IBeaconService beaconService;
        private IMapper mapper;

        [SetUp]
        public async Task Setup()
        {
            options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TrailsDbContext(options);
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))));
            beaconService = new BeaconService(context, mapper);
            await context.Beacons.AddRangeAsync(GetTestBeacons());
            await context.Participants.AddRangeAsync(GetTestParticipants());
            await context.SaveChangesAsync();
        }
            
        [Test]
        public async Task GetAllBeaconsReturnsCorrect()
        {
            var result = await beaconService.GetAllBeaconsAsync();
            Assert.AreEqual(ExpectedTotalBeaconsCount, result.TotalBeacons);
        }

        [Test]
        public async Task GetBeaconsPerPageReturnsCorrectCountOfBeacons()
        {
            var result = await beaconService.GetAllBeaconsAsync();
            Assert.AreEqual(ExpectedBeaconsPerPageCount, result.BeaconsPerPage);
        }

        [Test]
        public async Task GetBeaconsPerPageReturnsCorrectlyWhenPagedBeaconsAreNotEnoughForFullPage()
        {
            var result = await beaconService.GetAllBeaconsAsync(CurrentPage, BeaconsPerPage);
            Assert.AreEqual(ExpectedBeaconsInPage, result.Beacons.Count);
        }

        [Test]
        public async Task GetBeaconToEditReturnsEntityCorrectResult()
        {
            var result = await beaconService.GetBeaconToEditByIdAsync(CorrectBeaconId);
            Assert.IsNotNull(result);
            Assert.AreEqual(ExpectedBeaconImei, result.Imei);
        }

        [Test]
        public async Task GetBeaconToEditShouldReturnNullWhenIdIsIncorrectOrMissing()
        {
            var result = await beaconService.GetBeaconToEditByIdAsync(IncorrectBeaconId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateBeaconReturnsTrueWhenDataIsCorrect()
        {
            var result = await beaconService.CreateBeaconAsync(CorrectBeaconCreateTest());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateBeaconReturnsFalseWhenBeaconExists()
        {
            var result = await beaconService.CreateBeaconAsync(IncorrectBeaconCreateTest());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditBeaconReturnsTrueIfIdIsCorrect()
        {
            var result =
                await beaconService.EditBeaconAsync(NotInUseExistingBeaconId, CorrectBeaconCreateTest());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EditBeaconReturnsFalseWhenIdDoesNotExist()
        {
            var result =
                await beaconService.EditBeaconAsync(IncorrectBeaconId, CorrectBeaconCreateTest());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditBeaconShouldReturnFalseIfBeaconIsInUseAndWithCorrectId()
        {
            var result = await beaconService.EditBeaconAsync(ExistingBeaconId, CorrectBeaconCreateTest());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteBeaconShouldReturnTrueIfBeaconIsExistsAndNotInUse()
        {
            var result = await beaconService.DeleteBeaconAsync(NotInUseExistingBeaconId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteBeaconShouldReturnFalseIfBeaconExistsAndInUse()
        {
            var result = await beaconService.DeleteBeaconAsync(ExistingBeaconId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteBeaconShouldReturnFalseIfBeaconDoesNotExists()
        {
            var result = await beaconService.DeleteBeaconAsync(IncorrectBeaconId);
            Assert.IsFalse(result);
        }
    }
}
