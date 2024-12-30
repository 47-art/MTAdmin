using Microsoft.AspNetCore.Identity;
using MTAdmin.Infra.Identity.Entities;
using MTAdmin.Infra.Identity;
using MTAdmin.Application.Services.Interfaces;
using NToastNotify;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Memory;
using MTAdmin.Web.Areas.Archone.Services;
using MTAdmin.Web.Middlewares;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.Providers;
using MTAdmin.Web.Services;
using MTClient.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.TopRight
}).AddRazorOptions(options =>
{
    options.ViewLocationFormats.Add("/{0}.cshtml");
});

MTAdmin.Infra.Dependencies.InfraServices(builder.Configuration, builder.Services);
MTAdmin.Application.Dependencies.AppServices(builder.Configuration, builder.Services);
MTClient.Application.Dependencies.AppServices(builder.Configuration, builder.Services);
builder.Services.AddScoped<IHttpContextService, HttpContextService>();

builder.Services.AddImageSharp(opt =>
{
    opt.Configuration = Configuration.Default;
    opt.BrowserMaxAge = TimeSpan.FromDays(7);
    opt.CacheMaxAge = TimeSpan.FromDays(30);
    opt.CacheHashLength = 8;
    opt.Configuration.MemoryAllocator = MemoryAllocator.Create(new MemoryAllocatorOptions()
    {
        MaximumPoolSizeMegabytes = 64
    });
    opt.UseInvariantParsingCulture = true;
    opt.OnParseCommandsAsync = c =>
    {
        if (c.Commands.Count == 0)
        {
            return Task.CompletedTask;
        }

        // It's a good idea to have this to provide very basic security.
        // We can safely use the static resize processor properties.
        uint width = c.Parser.ParseValue<uint>(
            c.Commands.GetValueOrDefault(ResizeWebProcessor.Width),
            c.Culture);

        uint height = c.Parser.ParseValue<uint>(
            c.Commands.GetValueOrDefault(ResizeWebProcessor.Height),
            c.Culture);

        if (width > 4000 && height > 4000)
        {
            c.Commands.Remove(ResizeWebProcessor.Width);
            c.Commands.Remove(ResizeWebProcessor.Height);
        }

        return Task.CompletedTask;
    };
}).Configure<PhysicalFileSystemCacheOptions>(options => 
{    
    options.CacheFolderDepth = 12;
}).ClearProviders().AddProvider<WebRootImageProvider>();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.AllowedForNewUsers = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequiredUniqueChars = 1;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
}).AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>().
   AddEntityFrameworkStores<IdentityContext>().
   AddDefaultTokenProviders().
   AddSignInManager<AuditableSignInManager<AppUser>>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "Ar.Cookie";
    opt.Cookie.IsEssential = true;
    opt.Cookie.MaxAge = TimeSpan.FromHours(1.0);
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.SlidingExpiration = true;
    opt.Cookie.HttpOnly = true;
});

builder.Services.Configure<SiteAnalyticsOptions>(builder.Configuration.GetSection(nameof(SiteAnalyticsOptions)));
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IFileService,FileService>();
builder.Services.AddTransient<SiteAnalyticMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseNToastNotify();
app.UseAuthentication();
app.UseAuthorization();

app.UseSiteAnalytics();

app.MapControllerRoute(
    name: "Archone",
    pattern: "{area:exists}/{controller=Auth}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

app.UseImageSharp();
//app.Logger.LogInformation("App build successfully");
//app.Logger.LogInformation("Seeding database");

//try
//{
//await IdenitySeed.AddAdminUserIfNot(app.Services);
//await IdenitySeed.AddAdminRoleIfNot(app.Services);
//await IdenitySeed.AssignAdminRoleToAdminIfNot(app.Services);
//await AppDbSeed.AddDefaultPageMetaTagData(app.Services);
//var countries = await JsonFileReader.ReadAsync<MTAdmin.Shared.Models.CountryList>($"{builder.Environment.WebRootPath}\\Jsons\\Countries.json");
//await AppDbSeed.AddCountriesInfo(app.Services, countries.Countries);
//}
//catch (Exception ex)
//{
//    app.Logger.LogError(ex, "An error occurred while seeding the DB.");
//}

app.Run();
