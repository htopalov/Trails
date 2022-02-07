using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Web.Common;
using Trails.Web.Models.Route;
using Trails.Web.Services.Route;

namespace Trails.Web.Controllers
{
    [Authorize]
    public class RouteController : Controller
    {
        private readonly IRouteService routeService;

        public RouteController(IRouteService routeService) 
            => this.routeService = routeService;

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RouteCreateModel routeCreateModel)
        {
            if (!ModelState.IsValid)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.MissingRoutePropertiesError;
                return BadRequest();
            }

            var created = await this.routeService
                .CreateRouteAsync(routeCreateModel);

            if (!created)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.RouteCreateError;
                return BadRequest();
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventCreateSuccess;
            return Ok();
        }
    }
}
