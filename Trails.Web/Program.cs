using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trails.Web.BeaconApi.Services.BeaconDataService;
using Trails.Web.BeaconApi.Services.BeaconService;
using Trails.Web.Data;
using Trails.Web.Data.DomainModels;
using Trails.Web.Infrastructure.Extensions;

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
    .AddTransient<IBeaconService, BeaconService>();

builder
    .Services
    .AddTransient<IBeaconDataService, BeaconDataService>();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
