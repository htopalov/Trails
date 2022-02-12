using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trails.Web.Common;
using Trails.Web.Data.DomainModels;
using Trails.Web.Models.Event;
using Trails.Web.Services.Event;

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
            return RedirectToAction("Create","Route", new {forEventId = resultId});
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

        public async Task<IActionResult> Delete(string id)
        {
            var changeState = await this.eventService
                .DeleteEventAsync(id);

            if (!changeState)
            {
                return View("Error");
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventDeleteSuccess;
            return RedirectToAction("All");
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
            return RedirectToAction("Details", new { eventId });
        }

        public async Task<IActionResult> ApproveParticipant(string participantId,string eventId)
        {
            var participantState = await this.eventService
                .ApproveParticipantAsync(participantId,eventId);

            if (!participantState)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.ParticipantApproveError;
                return RedirectToAction("Details", new {eventId});
            }

            return NoContent();
        }

        public async Task<IActionResult> EditImage(string eventId)
        {
            var imgFile = Request.Form.Files.First();

            if (imgFile == null)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.MissingEventImageError;
                return RedirectToAction("Details", new { eventId });
            }

            if (!ValidateImageExtension(imgFile))
            {
                TempData[NotificationConstants.TempDataKeyFail] = ErrorMessages.ImageFileExtensionError;
                return RedirectToAction("Details", new { eventId });
            }

            var currentUserId = this.userManager
                .GetUserId(this.User);

            var edited = await this.eventService
                .EditImageAsync(currentUserId, eventId, imgFile);

            if (!edited)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventImageEditError;
                return RedirectToAction("Details", new { eventId });
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventImageEditSuccess;
            return RedirectToAction("Details", new{eventId});
        }

        private bool ValidateImageExtension(IFormFile imgFile)
        { 
            string[] extensions = new[] {"jpg", "jpeg", "png"};

            var extension = Path.GetExtension(imgFile.FileName).TrimStart('.');

            return extensions.Any(e => e.EndsWith(extension));
        }
    }
}
