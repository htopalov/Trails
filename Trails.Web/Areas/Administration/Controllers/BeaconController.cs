using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Web.Areas.Administration.Models.Beacon;
using Trails.Web.Areas.Administration.Services.Beacon;
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

        public IActionResult All() 
            => View(/*allbeaconsmodelwithpaging*/);

        //[HttpPost]
        //public IActionResult Create(BeaconFormModel beaconFormModel)
        //{
        //}


        //public IActionResult Edit(string id)
        //{
        //}

        //[HttpPost]
        //public IActionResult Edit(string id, BeaconFormModel beaconFormModel)
        //{
        //}

        //public IActionResult Details(string id)
        //{

        //}

        //[HttpPost]
        //public IActionResult Delete(string id)
        //{
        //}



        public IActionResult GetKey() 
            => Json(new {Key = SecurityProvider.RandomBeaconKeyGenerator()});
    }
}
