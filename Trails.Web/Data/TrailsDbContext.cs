using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Trails.Web.Data
{
    public class TrailsDbContext : IdentityDbContext
    {
        public TrailsDbContext(DbContextOptions<TrailsDbContext> options)
            : base(options)
        {
        }
    }
}