using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Common;
using Trails.Models.Beacon;
using Trails.Security;
using Trails.Services.Beacon;

namespace Trails.Web.Areas.Administration.Controllers
{
    [Area(AdministratorConstants.AreaName)]
    [Authorize(Roles = AdministratorConstants.AdministratorRoleName)]
    public class BeaconController : Controller
    {
        private readonly IBeaconService beaconService;

        public BeaconController(IBeaconService beaconService)
            => this.beaconService = beaconService;

        public async Task<IActionResult> All([FromQuery] AllBeaconsModel query)
        {
            var allBeacons = await this.beaconService
                .GetAllBeaconsAsync(query.CurrentPage, query.BeaconsPerPage);

            return View(allBeacons);
        }

        public IActionResult Create() 
            => View();


        [HttpPost]
        public async Task<IActionResult> Create(BeaconFormModel beaconFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(beaconFormModel);
            }

            var created = await this.beaconService
                .CreateBeaconAsync(beaconFormModel);

            if (!created)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.BeaconExists;
                return View(beaconFormModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.BeaconCreated;
            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Edit(string id)
        {
            var beaconToEdit = await this.beaconService
                .GetBeaconToEditByIdAsync(id);

            return View(beaconToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, BeaconFormModel beaconFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(beaconFormModel);
            }

            var updated = await this.beaconService
                .EditBeaconAsync(id, beaconFormModel);

            if (!updated)
            {
                //most likely will never hit this case because it is mandatory to generate new key and it
                //will never be the same as the old one so change tracker will detect this and will update entity

                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.BeaconEditedFail;
                return View(beaconFormModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.BeaconEditedSuccess;
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var deleted =await this.beaconService
                .DeleteBeaconAsync(id);

            if (!deleted)
            {
                TempData[NotificationConstants.TempDataKeyWarning] = NotificationConstants.BeaconNotExisting;
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.BeaconDeletedSuccess;
            return RedirectToAction(nameof(All));
        }

        public IActionResult GetKey() 
            => Json(new {Key = SecurityProvider.RandomBeaconKeyGenerator()});
    }
}
