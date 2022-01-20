using Microsoft.AspNetCore.Mvc;
using Trails.Web.Areas.Administration.Services.Beacon;
using Trails.Web.BeaconSecurity;

namespace Trails.Web.Areas.Administration.Controllers
{

    public class BeaconController : Controller
    {
        private readonly IBeaconService beaconService;

        public BeaconController(IBeaconService beaconService)
            => this.beaconService = beaconService;

        public IActionResult GetKey()
        {
            return Json(new {Key = SecurityProvider.RandomBeaconKeyGenerator()});
        }
    }
}
