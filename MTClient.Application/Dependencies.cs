using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTClient.Application.Services.Comman;
using MTClient.Application.Services.Implementations;
using MTClient.Application.Services.Interfaces;

namespace MTClient.Application
{
    public static class Dependencies
    {
        public static void AppServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IAboutUsService, AboutUsService>();
            services.AddScoped<ISiteAnalyticsService, SiteAnalyticsService>();            
        }
    }
}
