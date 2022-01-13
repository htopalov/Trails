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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Event>()
                .HasOne(e => e.Route)
                .WithOne(r => r.Event)
                .HasForeignKey<Route>(r => r.Id);

            builder
                    .Entity<Team>()
                    .HasOne(t => t.Event)
                    .WithMany(e => e.Teams)
                    .HasForeignKey(t => t.EventId);

            builder
                    .Entity<User>()
                    .HasOne(u => u.Team)
                    .WithMany(t => t.Users)
                    .HasForeignKey(u => u.TeamId);

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

            builder
                .Entity<RoutePoint>()
                .HasOne(rp => rp.Route)
                .WithMany(r => r.RoutePoints)
                .HasForeignKey(rp => rp.RouteId);

            foreach (var relation in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}