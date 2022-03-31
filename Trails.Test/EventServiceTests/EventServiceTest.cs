using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Trails.Data;
using Trails.Infrastructure;
using Trails.Models.Event;
using Trails.Services.Event;
using static Trails.Test.EventServiceTests.EventServiceTestData;
using static Trails.Test.EventServiceTests.EventServiceTestConstants;

namespace Trails.Test.EventServiceTests
{
    public class EventServiceTest
    {
        private DbContextOptions<TrailsDbContext> options;
        private TrailsDbContext context;
        private IEventService eventService;
        private IMapper mapper;

        [SetUp]
        public async Task Setup()
        {
            options = new DbContextOptionsBuilder<TrailsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new TrailsDbContext(options);
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(MappingProfile))));
            eventService = new EventService(context, mapper);
            await context.Events.AddRangeAsync(GetTestEvents());
            await context.Users.AddRangeAsync(GetTestUsers());
            await context.Participants.AddRangeAsync(GetTestParticipants());
            await context.SaveChangesAsync();
        }

        [Test]
        public async Task CreateEventShouldReturnEventId()
        {
            var result = await eventService.CreateEventAsync(ValidEventFormModel());
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [Test]
        public async Task CreateEventShouldReturnEmptyStringIfEventExists()
        {
            var result = await eventService.CreateEventAsync(ExistingEventFormModel());
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [Test]
        public async Task GetEventShouldReturnNullIfEventDoesNotExist()
        {
            var resultForDetailsModel = await eventService.GetEventAsync<EventDetailsModel>(InvalidEventId);
            var resultForEditModel = await eventService.GetEventAsync<EventEditFormModel>(InvalidEventId);
            Assert.IsNull(resultForDetailsModel);
            Assert.IsNull(resultForEditModel);
        }

        [Test]
        public async Task GetEventShouldReturnModelIfEventExists()
        {
            var resultForDetailsModel = await eventService.GetEventAsync<EventDetailsModel>(ValidEventId);
            var resultForEditModel = await eventService.GetEventAsync<EventEditFormModel>(ValidEventId);
            Assert.IsNotNull(resultForDetailsModel);
            Assert.IsNotNull(resultForEditModel);
        }

        [Test]
        public async Task GetEventShouldReturnSameTypeOfObjectPassedAsGenericForDetailsModel()
        {
            var detailsModel = await eventService.GetEventAsync<EventDetailsModel>(ValidEventId);
            Assert.IsInstanceOf<EventDetailsModel>(detailsModel);
        }

        [Test]
        public async Task GetEventShouldReturnSameTypeOfObjectPassedAsGenericForEditFormModel()
        {
            var formModel = await eventService.GetEventAsync<EventEditFormModel>(ValidEventId);
            Assert.IsInstanceOf<EventEditFormModel>(formModel);
        }

        [Test]
        public async Task GetEventShouldReturnPropertyForTypeOfDetailsModel()
        {
            var result = await eventService.GetEventAsync<EventDetailsModel>(ValidEventId);
            Assert.AreEqual(ExpectedFullNameOfCreator, result.CreatorFullName);
        }

        [Test]
        public async Task EditEventShouldReturnFalseIfEventDoesNotExist()
        {
            var result = await eventService.EditEventAsync(InvalidEventId, ValidEventEditFormModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditEventShouldReturnTrueIfEventExistsAndInFuture()
        {
            var result = await eventService.EditEventAsync(ValidFutureEventId, ValidEventEditFormModel());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EditEventShouldReturnFalseIfEventExistsAndHasExpired()
        {
            var result = await eventService.EditEventAsync(ValidExpiredEventId, ValidEventEditFormModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditEventShouldReturnFalseIfEventExistsAndLive()
        {
            var result = await eventService.EditEventAsync(ValidLiveEventId, ValidEventEditFormModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteEventShouldReturnFalseIfEventDoesNotExist()
        {
            var result = await eventService.DeleteEventAsync(InvalidEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteEventShouldReturnFalseIfEventIsCurrentlyLive()
        {
            var result = await eventService.DeleteEventAsync(ValidLiveEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteEventShouldReturnTrueIfEventHasExpired()
        {
            var result = await eventService.DeleteEventAsync(ValidExpiredEventId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteEventShouldReturnTrueIfEventIsInFuture()
        {
            var result = await eventService.DeleteEventAsync(ValidFutureEventId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnFalseIfEventDoesNotExist()
        {
            var result = await eventService.ApplyForEventAsync(ValidUserId, InvalidEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnFalseIfEventHasExpired()
        {
            var result = await eventService.ApplyForEventAsync(ValidUserId, ValidExpiredEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnFalseIfEventIsCurrentlyLive()
        {
            var result = await eventService.ApplyForEventAsync(ValidUserId, ValidLiveEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnTrueIfEventIsInFutureAndNotLocked()
        {
            var result = await eventService.ApplyForEventAsync(ValidUserId, ValidFutureEventId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnFalseIfUserIdIsInvalid()
        {
            var result = await eventService.ApplyForEventAsync(InvalidUserId, ValidFutureEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnFalseIfParticipantWithSameUserIdHasAppliedAlready()
        {
            var result = await eventService.ApplyForEventAsync(ValidExistingParticipantUserId, ValidFutureEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApplyForEventShouldReturnTrueIfInputIsCorrect()
        {
            var result = await eventService.ApplyForEventAsync(ValidUserId, ValidFutureEventId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApproveParticipantShouldReturnFalseIfEventOfParticipantDoesNotExist()
        {
            var result = await eventService.ApproveParticipantAsync(ValidExistingParticipantId, InvalidEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApproveParticipantShouldReturnFalseIfParticipantIdIsNotValid()
        {
            var result = await eventService.ApproveParticipantAsync(InvalidParticipantId, ValidFutureEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApproveParticipantShouldReturnFalseIfEventIsLocked()
        {
            var result = await eventService.ApproveParticipantAsync(ValidExistingParticipantId, ValidLiveEventId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ApproveParticipantShouldReturnTrueIfIdAndEventCorrect()
        {
            var result = await eventService.ApproveParticipantAsync(ValidParticipantIdAwaitingApprove, ValidFutureEventId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EditEventImageShouldReturnFalseIfEventDoesNotExist()
        {
            var result = await eventService.EditImageAsync(InvalidEventId, new EventImageEditModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditEventImageShouldReturnFalseIfEventIsLocked()
        {
            var result = await eventService.EditImageAsync(ValidLiveEventId, new EventImageEditModel());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task EditEventImageShouldReturnTrueIfImageIsValid()
        {
            var result = await eventService.EditImageAsync(ValidFutureEventId, ValidTestImage());
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetClosestEventsShouldReturnCorrectCountOfEvents()
        {
            var result = await eventService.GetClosestStartingEventsAsync();
            Assert.AreEqual(ExpectedCountOfClosestEvents, result.Count);
        }

        [Test]
        public async Task GetEventsShouldReturnAllEventsWhenNoUserIdIsAndNoEventsPerPageIsPassedToMethod()
        {
            var result = await eventService.GetEventsAsync();
            Assert.AreEqual(ExpectedCountOfPublicEvents, result.TotalEvents);
        }

        [Test]
        public async Task GetEventsShouldReturnAllEventsOfUserIfUserIdIsPassedAndEventsPerPageAreNotPassed()
        {
            var result = await eventService.GetEventsAsync(ValidUserId);
            Assert.AreEqual(ExpectedCountOfPersonalEvents,result.TotalEvents);
        }

        [Test]
        public async Task GetEventsShouldReturnWantedEventsBySearchTerm()
        {
            var result = await eventService.GetEventsAsync(searchEvent: SearchTerm);
            Assert.AreEqual(ExpectedCountOfResultAfterSearch, result.TotalEvents);
        }

        [Test]
        public async Task GetEventsShouldReturnAllEventsIfSearchTermIsWhiteSpace()
        {
            var result = await eventService.GetEventsAsync(searchEvent: " ");
            Assert.AreEqual(ExpectedCountOfPublicEvents, result.TotalEvents);
        }

        [Test]
        public async Task GetEventsShouldReturnExpectedCountPerPage()
        {
            var result = await eventService.GetEventsAsync(currentPage: CurrentPage, eventsPerPage: EventsPerPage);
            Assert.AreEqual(ExpectedCountOfEventsForPage,result.Events.Count);
        }

        [Test]
        public async Task GetLiveEventsShouldReturnCorrectCountOfEvents()
        {
            var result = await eventService.GetLiveEventsAsync();
            Assert.AreEqual(ExpectedCountOfLiveEvents, result.Count);
        }

        [Test]
        public async Task GetLiveEventShouldReturnNullIfEventIdIsInvalid()
        {
            var result = await eventService.GetLiveEventAsync(InvalidEventId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetLiveEventShouldReturnEventWithValidParticipants()
        {
            var result = await eventService.GetLiveEventAsync(ValidLiveEventId);
            Assert.IsNotNull(result);
            Assert.AreEqual(ExpectedCountOfParticipants, result.Participants.Count);
        }

        [Test]
        public async Task GetLiveEventShouldReturnEventWithValidRoutePointsForRoute()
        {
            var result = await eventService.GetLiveEventAsync(ValidLiveEventId);
            Assert.AreEqual(ExpectedCountOfRoutePoints, result.RoutePoints.Count);
        }

        [Test]
        public async Task GetLiveEventShouldReturnParticipantWithCorrectCountOfBeaconData()
        {
            var result = await eventService.GetLiveEventAsync(ValidLiveEventId);
            Assert.AreEqual(
                ExpectedCountOfBeaconData,
                result.Participants.FirstOrDefault(p=>p.Id == ParticipantIdInLiveEvent).BeaconData.Count);
        }

        [Test]
        public async Task GenerateParticipantPathShouldReturnNullIfParticipantDoesNotExist()
        {
            var result = await eventService.GenerateParticipantPathAsync(InvalidParticipantId);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GenerateParticipantPathShouldNullIfBeaconDataDoesNotExist()
        {
            var result = await eventService.GenerateParticipantPathAsync(ValidParticipantIdFromPassedEvent);
            Assert.IsNull(result);
        }

        [Test]
        public async Task GenerateParticipantPathShouldReturnProcessedPointsAsByteArray()
        {
            var result = await eventService.GenerateParticipantPathAsync(ParticipantIdInLiveEvent);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }
    }
}
