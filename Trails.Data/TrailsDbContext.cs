using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trails.Data.DomainModels;

namespace Trails.Data
{
    public class TrailsDbContext : IdentityDbContext<User>
    {
        public TrailsDbContext(DbContextOptions<TrailsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Beacon> Beacons { get; set; }

        public DbSet<BeaconData> BeaconData { get; set; }

        public DbSet<RoutePoint> RoutePoints { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var dbRelations = builder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys());

            foreach (var relation in dbRelations)
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<Beacon>()
                .HasMany(b => b.BeaconData)
                .WithOne(bd => bd.Beacon)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}