using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Web.Areas.Administration.Models.Beacon;
using Trails.Web.Areas.Administration.Services.Beacon;
using Trails.Web.Common;
using Trails.Web.Infrastructure;

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


        //public IActionResult Edit(string id)
        //{
        //}

        //[HttpPost]
        //public IActionResult Edit(string id, BeaconFormModel beaconFormModel)
        //{
        //}

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
