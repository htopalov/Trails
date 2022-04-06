using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Trails.Data;

namespace Trails.Test.Web.Helpers
{
    public class WebAppFactoryWithoutAuth<T> : WebApplicationFactory<Program>
        where T : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<TrailsDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TrailsDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<TrailsDbContext>();
                appContext.Database.EnsureCreated();

                services.AddAntiforgery(x =>
                {
                    x.Cookie.Name = AfTokenExtractor.AntiForgeryCookieName;
                    x.FormFieldName = AfTokenExtractor.AntiForgeryFieldName;
                });
            });
        }
    }
}
