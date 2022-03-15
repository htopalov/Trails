using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trails.Data.DomainModels;
using Trails.Models.Event;
using Trails.Services.Event;
using Route = Trails.Data.DomainModels.Route;
using static Trails.Common.NotificationConstants;

namespace Trails.Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly UserManager<User> userManager;

        public EventController(IEventService eventService, UserManager<User> userManager)
        {
            this.eventService = eventService;
            this.userManager = userManager;
        }

        public IActionResult Create() 
            => View();

        [HttpPost]
        public async Task<IActionResult> Create(EventFormModel eventFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(eventFormModel);
            }

            eventFormModel.CreatorId = this.userManager
                .GetUserId(this.User);

            var resultId = await this.eventService
                .CreateEventAsync(eventFormModel);

            if (resultId == string.Empty)
            {
                TempData[TempDataKeyFail] = EventExists;
                return View(eventFormModel);
            }

            TempData[TempDataKeySuccess] = EventCreateSuccess;
            return RedirectToAction(nameof(Create),nameof(Route), new {forEventId = resultId});
        }

        public async Task<IActionResult> Details(string eventId)
        {
            var @event = await this.eventService
                .GetEventAsync<EventDetailsModel>(eventId);

            if (@event == null)
            {
                return View("Error");
            }

            return View(@event);
        }

        public async Task<IActionResult> Delete(string eventId)
        {
            var hasChangedState = await this.eventService
                .DeleteEventAsync(eventId);

            if (!hasChangedState)
            {
                TempData[TempDataKeyFail] = EventDeleteFail;
                return RedirectToAction(nameof(Details), new {eventId});
            }

            TempData[TempDataKeySuccess] = EventDeleteSuccess;
            return RedirectToAction(nameof(Events));
        }

        public async Task<IActionResult> Apply(string userId, string eventId)
        {
            var hasApplied = await this.eventService
                .ApplyForEventAsync(userId, eventId);

            if (!hasApplied)
            {
                TempData[TempDataKeyFail] = ParticipantAlreadyAppliedError;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            TempData[TempDataKeySuccess] = ParticipantApplicationSuccess;
            return RedirectToAction(nameof(Details), new { eventId });
        }

        public async Task<IActionResult> ApproveParticipant(string participantId,string eventId)
        {
            var approveParticipantState = await this.eventService
                .ApproveParticipantAsync(participantId,eventId);

            if (!approveParticipantState)
            {
                TempData[TempDataKeyFail] = ParticipantApproveError;
                return RedirectToAction(nameof(Details), new {eventId});
            }

            return NoContent();
        }

        public async Task<IActionResult> EditImage(string eventId, EventImageEditModel imageModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[TempDataKeyFail] = ImageFileExtensionError;
                return RedirectToAction(nameof(Details), new {eventId});
            }

            var isEdited = await this.eventService
                .EditImageAsync(eventId, imageModel);

            if (!isEdited)
            {
                TempData[TempDataKeyFail] = EventImageEditError;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            TempData[TempDataKeySuccess] = EventImageEditSuccess;
            return RedirectToAction(nameof(Details), new { eventId });
        }

        public async Task<IActionResult> Edit(string eventId)
        {
            var eventToEdit = await this.eventService
                .GetEventAsync<EventEditFormModel>(eventId);

            if (eventToEdit == null)
            {
                return View("Error");
            }

            return View(eventToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string eventId, EventEditFormModel eventEditFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(eventEditFormModel);
            }

            var isEdited = await this.eventService
                .EditEventAsync(eventId, eventEditFormModel);

            if (!isEdited)
            {
                TempData[TempDataKeyFail] = EventEditFail;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            TempData[TempDataKeySuccess] = EventEditSuccess;
            return RedirectToAction(nameof(Details),new{eventId});
        }

        public async Task<IActionResult> Events(ListEventsModel eventsQuery)
        {
            var events = await this.eventService
                .GetEventsAsync(
                    eventsQuery.UserId,
                    eventsQuery.SearchEvent,
                    eventsQuery.CurrentPage,
                    eventsQuery.EventsPerPage);

            return View(events);
        }

        public async Task<IActionResult> Live()
        {
            var events = await this.eventService
                .GetLiveEventsAsync();

            return View(events);
        }

        public async Task<IActionResult> Broadcast(string eventId)
        {
            var @event = await this.eventService
                .GetLiveEventAsync(eventId);

            if (@event == null)
            {
                return View("Error");
            }

            return View(@event);
        }

        public async Task<IActionResult> DownloadActivity(string participantId)
        {
            var result = await this.eventService
                .GenerateParticipantPathAsync(participantId);

            if (result == null)
            {
                return View("Error");
            }

            return File(result, "application/force-download", $"TrailsLiveActivity-{participantId}-{DateTime.UtcNow:dd-MM-yyyy}.gpx");
        }
    }
}
