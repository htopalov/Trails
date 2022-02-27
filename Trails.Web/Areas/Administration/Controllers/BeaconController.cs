using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Models.Beacon;
using Trails.Security;
using Trails.Services.Beacon;
using static Trails.Common.NotificationConstants;
using static Trails.Web.Areas.Administration.AdministratorConstants;

namespace Trails.Web.Areas.Administration.Controllers
{
    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
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
                TempData[TempDataKeyFail] = BeaconExists;
                return View(beaconFormModel);
            }

            TempData[TempDataKeySuccess] = BeaconCreated;
            return RedirectToAction(nameof(All));
        }


        public async Task<IActionResult> Edit(string id)
        {
            var beaconToEdit = await this.beaconService
                .GetBeaconToEditByIdAsync(id);

            if (beaconToEdit == null)
            {
                return View("Error");
            }

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
                TempData[TempDataKeyFail] = BeaconNotExistingOrInUse;
                return RedirectToAction(nameof(All));
            }

            TempData[TempDataKeySuccess] = BeaconEditedSuccess;
            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var deleted =await this.beaconService
                .DeleteBeaconAsync(id);

            if (!deleted)
            {
                TempData[TempDataKeyFail] = BeaconNotExistingOrInUse;
                return RedirectToAction(nameof(All));
            }

            TempData[TempDataKeySuccess] = BeaconDeletedSuccess;
            return RedirectToAction(nameof(All));
        }

        public IActionResult GetKey() 
            => Json(new {Key = SecurityProvider.RandomBeaconKeyGenerator()});
    }
}
