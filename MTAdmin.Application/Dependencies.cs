using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTAdmin.Application.Services.Implementations;
using MTAdmin.Application.Services.Interfaces;

namespace MTAdmin.Application
{
    public static class Dependencies
    {
        public static void AppServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEmailSubscriberService, EmailSubscriberService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IUserAuditService, UserAuditService>();            
        }
    }
}
