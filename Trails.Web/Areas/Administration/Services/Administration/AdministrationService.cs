using Microsoft.EntityFrameworkCore;
using Trails.Web.Data;

namespace Trails.Web.Areas.Administration.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private TrailsDbContext dbContext;

        public AdministrationService(TrailsDbContext dbContext) 
            => this.dbContext = dbContext;

        public async Task<int> GetUnapprovedEventsCount() =>
            await this.dbContext
                .Events
                .Where(e => e.IsPublic == false)
                .CountAsync();
    }
}
