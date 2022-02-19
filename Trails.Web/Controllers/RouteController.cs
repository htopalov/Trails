using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Common;
using Trails.Models.Route;
using Trails.Services.Route;

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
            if (routeCreateModel.RoutePoints.Count == 0 || routeCreateModel.Length == 0)
            {
                ModelState.AddModelError(string.Empty, string.Empty);
            }

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

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.RouteCreateSuccess;
            return Ok();
        }

        public async Task<IActionResult> Details(string routeId)
        {
            var routeDetailsModel = await this.routeService
                .GetRouteAsync(routeId);

            if (routeDetailsModel == null)
            {
                return View("Error");
            }

            return View(routeDetailsModel);
        }

        public async Task<IActionResult> Edit(string routeId)
        {
            var routeToEdit = await this.routeService
                .GetRouteToEditAsync(routeId);

            if (routeToEdit == null)
            {
                return View("Error");
            }

            return View(routeToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string routeId, RouteEditFormModel routeEditFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(routeEditFormModel);
            }

            var updated = await this.routeService
                .EditRouteAsync(routeId, routeEditFormModel);

            if (!updated)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.RouteEditFail;
                return View(routeEditFormModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.RouteEditSuccess;
            return RedirectToAction(nameof(Details), new { routeId });
        }

        public async Task<IActionResult> All(AllRoutesModel queryModel)
        {
            var allRoutes = await this.routeService
                .GetAllRoutesAsync(queryModel.CurrentPage, queryModel.RoutesPerPage);

            return View(allRoutes);
        }

        public async Task<IActionResult> Download(string routeId)
        {
            var result = await this.routeService
                .GenerateGPXAsync(routeId);

            return File(result, "application/force-download", $"TrailsLiveActivity-{routeId}-{DateTime.UtcNow:dd-MM-yyyy}.gpx");
        }
    }
}
