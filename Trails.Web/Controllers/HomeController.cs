using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Trails.Data.DomainModels;
using Trails.Services.Event;

namespace Trails.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string AnonymousIndexImageCacheKey = 
            nameof(AnonymousIndexImageCacheKey);

        private readonly IWebHostEnvironment env;
        private readonly SignInManager<User> manager;
        private readonly IEventService eventService;
        private readonly IMemoryCache cache;

        public HomeController(
            IWebHostEnvironment env,
            SignInManager<User> manager,
            IEventService eventService,
            IMemoryCache cache)
        {
            this.env = env;
            this.manager = manager;
            this.eventService = eventService;
            this.cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (!manager.IsSignedIn(User))
            {
                var imageFileNames = this.cache
                    .Get<string[]>(AnonymousIndexImageCacheKey);

                if (imageFileNames == null)
                {
                    imageFileNames = Directory
                        .GetFiles(env.WebRootPath + "\\images")
                        .Select(f => Path.GetFileName(f))
                        .ToArray();

                    var cacheOpt = new MemoryCacheEntryOptions
                    {
                        Size = 10240,
                        AbsoluteExpiration = DateTime.UtcNow.AddHours(12)
                    };

                    this.cache
                        .Set(AnonymousIndexImageCacheKey, imageFileNames, cacheOpt);
                }

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