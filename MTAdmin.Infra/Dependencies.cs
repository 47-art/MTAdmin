using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MTAdmin.Core.Interfaces;
using MTAdmin.Infra.Dapper;
using MTAdmin.Infra.Identity;
using MTAdmin.Infra.Repository.Identity;
using MTAdmin.Infra.Repository;
using MTAdmin.Infra.Data;

namespace MTAdmin.Infra
{
    public static class Dependencies
    {        
        public static void InfraServices(IConfiguration configuration, IServiceCollection services)
        {            
            string MigrationProjName = "MTAdmin.Migrations";
            string appConnectionStrKey = "AppConnection";
            string identityConnectionStrKey = "IdentityConnection";

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(appConnectionStrKey), x =>
                {
                    x.MigrationsAssembly(MigrationProjName);
                });
            });
            services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(identityConnectionStrKey), x =>
                {
                    x.MigrationsAssembly(MigrationProjName);
                });
            });
            services.AddSingleton<DapperContext>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
