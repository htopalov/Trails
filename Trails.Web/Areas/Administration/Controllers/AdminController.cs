using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Models.Event;
using Trails.Models.Participant;
using Trails.Services.Administration;
using static Trails.Common.NotificationConstants;
using static Trails.Web.Areas.Administration.AdministratorConstants;

namespace Trails.Web.Areas.Administration.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
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
                TempData[TempDataKeyFail] = EventApproveFail;
            }

            TempData[TempDataKeySuccess] = EventApproveSuccess;
            return RedirectToAction(nameof(ManageEvents));
        }

        public async Task<IActionResult> DeclineEvent(string eventId)
        {
            var updated = await this.adminService
                .DeclineEventAsync(eventId);

            if (!updated)
            {
                TempData[TempDataKeyFail] = EventDeclineFail;
            }

            TempData[TempDataKeySuccess] = EventDeclineSuccess;
            return RedirectToAction(nameof(ManageEvents));
        }

        public async Task<IActionResult> DetachBeacons()
        {
            var detached = await this.adminService
                .DetachBeaconsFromParticipantsInPassedEventsAsync();
            if (!detached)
            {
                return View("Error");
            }

            return NoContent();
        }

        public async Task<IActionResult> Connectivity()
        {
            var events = await this.adminService
                .GetEventsToPrepareAsync();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Connectivity([FromBody]ParticipantBeaconModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await this.adminService
                .ConnectBeaconToParticipantAsync(request);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        public async Task<IActionResult> GetParticipantsForEvent(string eventId)
        {
            var result = await this.adminService
                .GetParticipantsToPrepareAsync(eventId);

            if (result == null)
            {
                return BadRequest();
            }

            return Json(result);
        }

        public async Task<IActionResult> GetBeaconsForEvent()
        {
            var beacons = await this.adminService
                .GetBeaconsToConnectAsync();

            return Json(beacons);
        }
    }
}
