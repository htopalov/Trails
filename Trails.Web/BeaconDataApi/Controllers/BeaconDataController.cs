using Microsoft.AspNetCore.Mvc;
using Trails.Web.BeaconDataApi.Filters;
using Trails.Web.BeaconDataApi.Models;
using Trails.Web.BeaconDataApi.Services.BeaconDataService;

namespace Trails.Web.BeaconDataApi.Controllers
{
    [ApiController]
    [Route("beacon/data")]
    public class BeaconDataController : ControllerBase
    {
        private readonly IBeaconDataService beaconDataService;

        public BeaconDataController(IBeaconDataService beaconDataService)
            => this.beaconDataService = beaconDataService;

        //Depending on what my decision which will be in a few days or a week I may need to implement logic
        //for deleting older data when not needed anymore. Easiest way will be with sql job and certain
        //time (for instance 6 months) because it will be automatic process which I will not handle manually.
        //The main idea here is not to be able to manipulate position data coming from beacons even if you are admin
        //while events occur and even after that because you may cause errors in passed routes by participants in events.
        //Need to think more over this topic...

        [ServiceFilter(typeof(AuthKeyFilter))]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(BeaconDataDtoPost beaconDataDto)
        {
            return Ok(new {Result = "Posted"});
        }
    }
}
