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

            var resultId = await this.eventService
                .CreateEventAsync(eventFormModel, currentUserId, imgFile);

            if (resultId == string.Empty)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventExists;
                return View(eventFormModel);
            }

            return RedirectToAction("Create","Route", new {id = resultId});
        }

        private bool ValidateImageExtension(IFormFile imgFile)
        { 
            string[] extensions = new[] {"jpg", "jpeg", "png"};

            var extension = Path.GetExtension(imgFile.FileName).TrimStart('.');

            return extensions.Any(e => e.EndsWith(extension));
        }
    }
}
