using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Trails.Data.DomainModels;
using Trails.Models.Contact;
using Trails.Services.Event;
using Trails.Services.User;
using static Trails.Common.NotificationConstants;

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
        private readonly IEmailService emailService;

        public HomeController(
            IWebHostEnvironment env,
            SignInManager<User> manager,
            IEventService eventService,
            IMemoryCache cache,
            IEmailService emailService)
        {
            this.env = env;
            this.manager = manager;
            this.eventService = eventService;
            this.cache = cache;
            this.emailService = emailService;
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

        public IActionResult Contact() 
            => View("Contact");

        [HttpPost]
        public async Task<IActionResult> Contact(ContactModel contactModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactModel);
            }

            var message = this.emailService
                .ContactMessage(contactModel);

            var result = await this.emailService
                .SendEmailAsync(message);

            if (!result)
            {
                TempData[TempDataKeyFail] = ContactFail;
                return RedirectToAction(nameof(Index));
            }

            TempData[TempDataKeySuccess] = ContactSuccess;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
            => View();

        public IActionResult Faq()
            => View();
    }
}