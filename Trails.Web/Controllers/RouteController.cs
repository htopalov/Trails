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

        public IActionResult Create(string id)
        {
            TempData["EventId"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RouteCreateModel routeCreateModel)
        {
            routeCreateModel.EventId = TempData["EventId"].ToString();
            var created = await this.routeService
                .CreateRouteAsync(routeCreateModel);
            if (!created)
            {
                TempData[NotificationConstants.TempDataKeyFail] = NotificationConstants.RouteCreateError;
                return View(routeCreateModel);
            }

            TempData[NotificationConstants.TempDataKeySuccess] = NotificationConstants.EventCreateSuccess;
            return View(); //return redirect to action event details for newly created event with route

        }
    }
}
