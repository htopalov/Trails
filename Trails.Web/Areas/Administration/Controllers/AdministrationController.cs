using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trails.Web.Areas.Administration.Services.Administration;

namespace Trails.Web.Areas.Administration.Controllers
{
    [Area(AdministratorConstants.AreaName)]
    [Authorize(Roles = AdministratorConstants.AdministratorRoleName)]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService adminService;

        public AdministrationController(IAdministrationService adminService) 
            => this.adminService = adminService;

        public async Task<IActionResult> Index()
            => View(await this.adminService.GetUnapprovedEventsCount());
    }
}
