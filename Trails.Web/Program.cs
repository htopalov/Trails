using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trails.Api.Hub;
using Trails.Data;
using Trails.Data.DomainModels;
using Trails.Infrastructure;
using Trails.Services.User;
using Trails.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
                        .Configuration
                        .GetConnectionString("DefaultConnection");

var emailConfig = builder
    .Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();

builder
    .Services
    .SetupServices();

builder
    .Services
    .AddScoped<IEmailService>(x => new EmailService(emailConfig));

builder
    .Services
    .AddDbContext<TrailsDbContext>(options =>
        options.UseSqlServer(connectionString));

builder
    .Services
    .AddDatabaseDeveloperPageExceptionFilter();

builder
    .Services
    .AddDefaultIdentity<User>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TrailsDbContext>()
    .AddDefaultTokenProviders();

builder
    .Services
    .Configure<DataProtectionTokenProviderOptions>(opts =>
        opts.TokenLifespan = TimeSpan.FromHours(1));

builder
    .Services
    .Configure<RouteOptions>(opt =>
    {
        opt.LowercaseUrls = true;
    });

builder
    .Services
    .AddMemoryCache();

builder
    .Services
    .AddSignalR();

builder
    .Services
    .AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

builder
    .Services
    .ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.MaxAge = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
    });

builder
    .Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "The field is required.");
    });

var app = builder.Build();

app.DatabaseSetup();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller}/{action}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<BroadcastHub>("/live-feed");
});

app.MapRazorPages();
app.Run();

public partial class Program { }
