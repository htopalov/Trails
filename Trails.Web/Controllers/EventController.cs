using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trails.Common;
using Trails.Data.DomainModels;
using Trails.Models.Event;
using Trails.Services.Event;
using Route = Trails.Data.DomainModels.Route;

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

            var currentUserId = this.userManager
                .GetUserId(this.User);

            var imgFile = Request.Form.Files.First();

            if (imgFile == null)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.MissingEventImageError;
                return View(eventFormModel);
            }

            if (!ValidateImageExtension(imgFile))
            {
                TempData[NotificationConstants.TempDataKeyFail] = ErrorMessages.ImageFileExtensionError;
                return View(eventFormModel);
            }

            if (eventFormModel.EndDate < eventFormModel.StartDate)
            {
                TempData[NotificationConstants.TempDataKeyFail] = ErrorMessages.InvalidStartEndDate;
                return View(eventFormModel);
            }

            if (DateTime.UtcNow > eventFormModel.StartDate.AddDays(-3))
            {
                TempData[NotificationConstants.TempDataKeyFail] = ErrorMessages.EventThreeDaysBeforeStartError;
                return View(eventFormModel);
            }

            var resultId = await this.eventService
                .CreateEventAsync(eventFormModel, currentUserId, imgFile);

            if (resultId == string.Empty)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventExists;
                return View(eventFormModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventCreateSuccess;
            return RedirectToAction(nameof(Create),nameof(Route), new {forEventId = resultId});
        }

        public async Task<IActionResult> Details(string eventId)
        {
            var @event = await this.eventService
                .GetEventAsync(eventId);

            if (@event == null)
            {
                return View("Error");
            }

            return View(@event);
        }

        public async Task<IActionResult> Delete(string eventId)
        {
            var changeState = await this.eventService
                .DeleteEventAsync(eventId);

            if (!changeState)
            {
                return View("Error");
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventDeleteSuccess;
            return RedirectToAction("All"); //TODO:change to nameof(All) when view is ready
        }

        public async Task<IActionResult> Apply(string userId, string eventId)
        {
            var hasApplied = await this.eventService
                .ApplyForEventAsync(userId, eventId);

            if (!hasApplied)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.ParticipantAlreadyAppliedError;
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.ParticipantApplicationSuccess;
            return RedirectToAction(nameof(Details), new { eventId });
        }

        public async Task<IActionResult> ApproveParticipant(string participantId,string eventId)
        {
            var participantState = await this.eventService
                .ApproveParticipantAsync(participantId,eventId);

            if (!participantState)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.ParticipantApproveError;
                return RedirectToAction(nameof(Details), new {eventId});
            }

            return NoContent();
        }

        public async Task<IActionResult> EditImage(string eventId)
        {
            var imgFile = Request.Form.Files.First();

            if (imgFile == null)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.MissingEventImageError;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            if (!ValidateImageExtension(imgFile))
            {
                TempData[NotificationConstants.TempDataKeyFail] = ErrorMessages.ImageFileExtensionError;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            var currentUserId = this.userManager
                .GetUserId(this.User);

            var edited = await this.eventService
                .EditImageAsync(currentUserId, eventId, imgFile);

            if (!edited)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventImageEditError;
                return RedirectToAction(nameof(Details), new { eventId });
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventImageEditSuccess;
            return RedirectToAction(nameof(Details), new{eventId});
        }

        public async Task<IActionResult> Edit(string eventId)
        {
            var eventToEdit = await this.eventService
                .GetEventToEditAsync(eventId);

            return View(eventToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string eventId, EventEditFormModel eventEditFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(eventEditFormModel);
            }

            var updated = await this.eventService
                .EditEventAsync(eventId, eventEditFormModel);

            if (!updated)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventEditFail;
                return View(eventEditFormModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventEditSuccess;
            return RedirectToAction(nameof(Details),new{eventId});
        }

        private bool ValidateImageExtension(IFormFile imgFile)
        { 
            string[] extensions = new[] {"jpg", "jpeg", "png"};

            var extension = Path.GetExtension(imgFile.FileName).TrimStart('.');

            return extensions.Any(e => e.EndsWith(extension));
        }
    }
}
