using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTAdmin.Infra.Identity.Configs;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity
{
    public class IdentityContext : IdentityDbContext<AppUser, AppRole,
                                                        string, AppUserClaim,
                                                        AppUserRole, AppUserLogin,
                                                        AppRoleClaim, AppUserToken>
    {
        public IdentityContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<AppRole> AppRoles => Set<AppRole>();
        public DbSet<AppUserRole> AppUserRoles => Set<AppUserRole>();
        public DbSet<AppUserClaim> AppUserClaims => Set<AppUserClaim>();
        public DbSet<AppUserLogin> AppUserLogins => Set<AppUserLogin>();
        public DbSet<AppUserToken> AppUserTokens => Set<AppUserToken>();
        public DbSet<AppRoleClaim> AppRoleClaims => Set<AppRoleClaim>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new AppRoleClaimConfig());
            builder.ApplyConfiguration(new AppRoleConfig());
            builder.ApplyConfiguration(new AppUserClaimConfig());
            builder.ApplyConfiguration(new AppUserLoginConfig());
            builder.ApplyConfiguration(new AppUserRoleConfig());
            builder.ApplyConfiguration(new AppUserTokenConfig());
        }
    }
}
