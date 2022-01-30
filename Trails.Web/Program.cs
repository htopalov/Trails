using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trails.Web.Areas.Administration.Services.Administration;
using Trails.Web.Areas.Administration.Services.Beacon;
using Trails.Web.BeaconDataApi.Filters;
using Trails.Web.BeaconDataApi.Services.BeaconDataService;
using Trails.Web.Data;
using Trails.Web.Data.DomainModels;
using Trails.Web.Services.Event;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder
                        .Configuration
                        .GetConnectionString("DefaultConnection");

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
    .AddEntityFrameworkStores<TrailsDbContext>();

builder
    .Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder
    .Services
    .AddTransient<AuthKeyFilter>();

builder
    .Services
    .AddTransient<IBeaconService, BeaconService>();

builder
    .Services
    .AddTransient<IAdministrationService, AdministrationService>();

builder
    .Services
    .AddTransient<IBeaconDataService, BeaconDataService>();

builder
    .Services
    .AddTransient<IEventService, EventService>();

builder
    .Services
    .AddControllersWithViews(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "The field is required.");
    });

var app = builder.Build();

//TODO: UNCOMMENT WHEN DEPLOYING 
//app.DatabaseSetup();

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

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller}/{action}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
