using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Trails.Data;
using Trails.Data.DomainModels;
using Trails.Services.Beacon;
using Trails.Services.Administration;
using Trails.Services.Event;
using Trails.Services.Route;
using Trails.Services.Statistics;
using Trails.Api.Services;
using Trails.Api.Filters;
using static Trails.Web.Areas.Administration.AdministratorConstants;

namespace Trails.Web
{
    public static class WebApplicationExtensions
    {
        public static IServiceCollection SetupServices(this IServiceCollection services) 
            => services
                .AddScoped<AuthKeyFilter>()
                .AddScoped<IBeaconService, BeaconService>()
                .AddScoped<IBeaconService, BeaconService>()
                .AddScoped<IAdministrationService, AdministrationService>()
                .AddScoped<IBeaconDataService, BeaconDataService>()
                .AddScoped<IEventService, EventService>()
                .AddScoped<IRouteService, RouteService>()
                .AddScoped<IStatisticsService, StatisticsService>();

        public static WebApplication DatabaseSetup(this WebApplication app)
        {
            using var serviceScope = app.Services
                .CreateScope();

            var services = serviceScope
                .ServiceProvider;

            var admin = app
                .Configuration
                .GetSection("AdministratorDetails")
                .Get<User>();

            var adminPassword = app
                .Configuration
                .GetValue<string>("AdministratorDetails:Password");


            MigrateDatabase(services);
            SeedAdministrator(services, admin, adminPassword);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services
                .GetRequiredService<TrailsDbContext>();

            data
                .Database
                .Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services, User admin, string adminPassword)
        {
            var userManager = services
                .GetRequiredService<UserManager<User>>();

            var roleManager = services
                .GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        throw new InvalidOperationException("Roles already exists");
                    }

                    var adminRole = new IdentityRole {Name = AdministratorRoleName};

                    var roleCreationResult =await roleManager
                        .CreateAsync(adminRole);

                    if (!roleCreationResult.Succeeded)
                    {
                        throw new InvalidOperationException("Role creation failed");
                    }

                    var adminUserCreationResult = await userManager
                        .CreateAsync(admin, adminPassword);

                    if (!adminUserCreationResult.Succeeded)
                    {
                        throw new InvalidOperationException("Admin user creation failed");
                    }

                    var adminRoleIncludeResult = await userManager
                        .AddToRoleAsync(admin, adminRole.Name);

                    if (!adminRoleIncludeResult.Succeeded)
                    {
                        throw new InvalidOperationException("Adding role to admin failed");
                    }
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
