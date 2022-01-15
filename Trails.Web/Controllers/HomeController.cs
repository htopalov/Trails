using Microsoft.AspNetCore.Mvc;

namespace Trails.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment env;

        public HomeController(IWebHostEnvironment env) 
            => this.env = env;

        public IActionResult Index()
        {
            var imageFileNames = Directory
                .GetFiles(env.WebRootPath + "\\images")
                .Select(f=> Path.GetFileName(f))
                .ToArray();
            return View(imageFileNames);
        }

        public IActionResult Error() => View();
    }
}