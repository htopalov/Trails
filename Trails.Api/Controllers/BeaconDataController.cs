using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Trails.Api.Filters;
using Trails.Api.Hub;
using Trails.Api.Models;
using Trails.Api.Services;

namespace Trails.Api.Controllers
{
    [ApiController]
    [Route("beacon/data")]
    public class BeaconDataController : ControllerBase
    {
        private readonly IBeaconDataService beaconDataService;
        private readonly IHubContext<BroadcastHub> hub;

        public BeaconDataController(
            IBeaconDataService beaconDataService,
            IHubContext<BroadcastHub> hub)
        {
            this.beaconDataService = beaconDataService;
            this.hub = hub;
        }

        [ServiceFilter(typeof(AuthKeyFilter))]
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(BeaconDataDtoPost beaconDataDto)
        {
            var result = await this.beaconDataService
                .CreateBeaconDataAsync(beaconDataDto);

            if (result == null)
            {
                return BadRequest();
            }

            await this.hub
                .Clients
                .All
                .SendAsync("BroadcastData",result);
   
            return Ok();
        }
    }
}
