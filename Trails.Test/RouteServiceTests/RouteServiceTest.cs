using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Trails.Data;
using Trails.Infrastructure;
using Trails.Models.Route;
using Trails.Services.Route;
using static Trails.Test.RouteServiceTests.RouteServiceTestData;
using static Trails.Test.RouteServiceTests.RouteServiceTestConstants;

namespace Trails.Test.RouteServiceTests
{
    public class RouteServiceTest
    {
        private DbContextOptions<TrailsDbContext> options;
        private TrailsDbContext context;
        private IRouteService routeService;
        private IMapper mapper;

        [SetUp]
        public async Task Setup()
        {
            options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TrailsDbContext(options);
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))));
            routeService = new RouteService(context, mapper);
            await context.Events.AddRangeAsync(GetTestEvents());
            await context.Routes.AddRangeAsync(GetTestRoutes());
            await context.RoutePoints.AddRangeAsync(GetTestRoutePoints());
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task CreateRouteShouldReturnFalseIfRouteWithSameNameAlreadyExists()
        {
            var result = await routeService.CreateRouteAsync(GetValidTestRouteCreateModelWithExistingName());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateRouteShouldReturnFalseIfEventForRouteDoesNotExist()
        {
            var result = await routeService.CreateRouteAsync(InvalidTestRouteCreateModelWithInvalidEventId());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateRouteShouldReturnTrueWhenRouteIsWithoutAltitudesIfNotProvided()
        {
            var result = await routeService.CreateRouteAsync(GetValidTestRouteCreateModelWithoutAltitudes());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateRouteShouldReturnTrueWhenRouteIsWithAltitudes()
        {
            var result = await routeService.CreateRouteAsync(GetValidTestRouteCreateModelWithAltitudes());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetRouteShouldReturnNullIfRouteDoesNotExist()
        {
            var resultForDetails = await routeService.GetRouteAsync<RouteDetailsModel>(InvalidRouteId);
            var resultForEdit = await routeService.GetRouteAsync<RouteEditFormModel>(InvalidRouteId);
            Assert.IsNull(resultForDetails);
            Assert.IsNull(resultForEdit);
        }

        [Test]
        public async Task GetRouteShouldReturnCorrectResultIfRouteExists()
        {
            var resultForDetails = await routeService.GetRouteAsync<RouteDetailsModel>(ValidRouteId);
            var resultForEdit = await routeService.GetRouteAsync<RouteEditFormModel>(ValidRouteId);
            Assert.IsNotNull(resultForDetails);
            Assert.IsNotNull(resultForEdit);
        }
        [Test]
        public async Task GetEventShouldReturnSameTypeOfObjectPassedAsGenericForDetailsModel()
        {
            var detailsModel = await routeService.GetRouteAsync<RouteDetailsModel>(ValidRouteId);
            Assert.IsInstanceOf<RouteDetailsModel>(detailsModel);
        }

        [Test]
        public async Task GetEventShouldReturnSameTypeOfObjectPassedAsGenericForEditFormModel()
        {
            var formModel = await routeService.GetRouteAsync<RouteEditFormModel>(ValidRouteId);
            Assert.IsInstanceOf<RouteEditFormModel>(formModel);
        }

        [Test]
        public async Task GetEventShouldReturnObjectWithCorrectCountOfRoutePoints()
        {
            var result = await routeService.GetRouteAsync<RouteDetailsModel>(ValidRouteId);
            Assert.AreEqual(ExpectedCountOfRoutePoints, result.RoutePoints.Count);
        }

        [Test]
        public async Task EditRouteShouldReturnFalseIfRouteIdIsInvalid()
        {
            var result = await routeService.EditRouteAsync(InvalidRouteId, ValidTestRouteEditModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditRouteShouldReturnTrueIfIdIsCorrect()
        {
            var result = await routeService.EditRouteAsync(ValidRouteId, ValidTestRouteEditModel());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAllRoutesShouldReturnTotalCountOfRoutes()
        {
            var result = await routeService.GetAllRoutesAsync();
            Assert.AreEqual(ExpectedTotalRoutesCount, result.TotalRoutes);
        }

        [Test]
        public async Task GetAllRoutesShouldReturnCorrectRouteWhenSearchTermIsProvided()
        {
            var result = await routeService.GetAllRoutesAsync(searchRoute: SearchTerm);
            Assert.AreEqual(ExpectedCountOfRoutesWhenSearching, result.Routes.Count);
        }

        [Test]
        public async Task GetAllRoutesShouldReturnAllRoutesWhenSearchTermIsEmptyString()
        {
            var result = await routeService.GetAllRoutesAsync(searchRoute: "");
            Assert.AreEqual(ExpectedCountOfRoutesWhenSearchingEmptyString, result.Routes.Count);
        }

        [Test]
        public async Task GetAllRoutesShouldReturnCorrectCountOfRoutesWhenPerPageIsPassed()
        {
            var result = await routeService.GetAllRoutesAsync(routesPerPage: RoutesPerPage);
            Assert.AreEqual(ExpectedRoutesCountWithRoutesPerPage, result.Routes.Count);
        }

        [Test]
        public async Task GenerateGpxShouldReturnNullIfRouteDoesNotExist()
        {
            var result = await routeService.GenerateGPXAsync(InvalidRouteId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GenerateGpxShouldReturnByteArrayWhenRouteExists()
        {
            var result = await routeService.GenerateGPXAsync(ValidRouteId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }
    }
}
