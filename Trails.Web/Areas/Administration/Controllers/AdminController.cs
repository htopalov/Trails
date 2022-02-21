using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Common;
using Trails.Models.Event;
using Trails.Services.Administration;

namespace Trails.Web.Areas.Administration.Controllers
{
    [Area(AdministratorConstants.AreaName)]
    [Authorize(Roles = AdministratorConstants.AdministratorRoleName)]
    public class AdminController : Controller
    {
        private readonly IAdministrationService adminService;

        public AdminController(IAdministrationService adminService) 
            => this.adminService = adminService;

        public async Task<IActionResult> Index()
            => View(await this.adminService.GetUnapprovedEventsCountAsync());

        public async Task<IActionResult> ManageEvents(AllUnapprovedEventsModel query)
        {
            var events = await this.adminService
                .GetAllUnapprovedEventsAsync(query.CurrentPage, query.EventsPerPage);

            return View(events);
        }

        public async Task<IActionResult> ApproveEvent(string eventId)
        {
            var updated = await this.adminService
                .ApproveEventAsync(eventId);

            if (!updated)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventApproveFail;
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventApproveSuccess;
            return RedirectToAction(nameof(ManageEvents));
        }

        public async Task<IActionResult> DeclineEvent(string eventId)
        {
            var updated = await this.adminService
                .DeclineEventAsync(eventId);

            if (!updated)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.EventDeclineFail;
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventDeclineSuccess;
            return RedirectToAction(nameof(ManageEvents));
        }
    }
}
