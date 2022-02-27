using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Trails.Data.DomainModels;
using Trails.Services.Event;

namespace Trails.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly SignInManager<User> manager;
        private readonly IEventService eventService;

        public HomeController(
            IWebHostEnvironment env,
            SignInManager<User> manager,
            IEventService eventService)
        {
            this.env = env;
            this.manager = manager;
            this.eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            if (!manager.IsSignedIn(User))
            {
                var imageFileNames = Directory
                    .GetFiles(env.WebRootPath + "\\images")
                    .Select(f => Path.GetFileName(f))
                    .ToArray();
                return View("_AnonymousUserPartial",imageFileNames);
            }

            var events = await this.eventService
                .GetClosestStartingEventsAsync();

            return View("_LoggedUserPartial", events);

        }

        public IActionResult Error() 
            => View();
    }
}