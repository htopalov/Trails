using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Trails.Web.Data.DomainModels;
using Route = Trails.Web.Data.DomainModels.Route;

namespace Trails.Web.Data
{
    public class TrailsDbContext : IdentityDbContext<User>
    {
        public TrailsDbContext(DbContextOptions<TrailsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<RoutePoint> RoutePoints { get; set; }

        public DbSet<Beacon> Beacons { get; set; }

        public DbSet<BeaconData> BeaconData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<UserEvent>()
                .HasKey(ue => new {ue.UserId, ue.EventId});

            builder
                .Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(e => e.UsersEvents)
                .HasForeignKey(eu => eu.UserId);

            builder
                    .Entity<UserEvent>()
                    .HasOne(ue => ue.Event)
                    .WithMany(e => e.UsersEvents)
                    .HasForeignKey(ue => ue.EventId);

            var dbRelations = builder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys());

            foreach (var relation in dbRelations)
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}