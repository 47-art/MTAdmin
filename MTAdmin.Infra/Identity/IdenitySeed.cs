using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MTAdmin.Infra.Identity.Entities;
using MTAdmin.Shared.Constants;

namespace MTAdmin.Infra.Identity
{
    public static class IdenitySeed
    {
        public static async Task AddAdminUserIfNot(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var scopedProvider = scope.ServiceProvider;
                try
                {
                    var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
                    var context = scopedProvider.GetRequiredService<IdentityContext>();

                    bool isExist = await context.AppUsers.AnyAsync(x => x.UserRoles.Any(x => x.Role.Name.ToLower() == Roles.Archon.ToLower()));

                    if (!isExist)
                    {
                        AppUser defaultUser = new AppUser()
                        {
                            Id = Guid.NewGuid().ToString(),
                            FirstName = "Jhon",
                            LastName = "Doe",
                            Email = "JhonAdmin@yopmail.com",
                            UserName = "JhonAdmin@yopmail.com",
                            EmailConfirmed = true,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                        };
                        defaultUser.CreatedById = defaultUser.Id;
                        await userManager.CreateAsync(defaultUser, "Cba#3210");
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        public static async Task AddAdminRoleIfNot(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var provider = scope.ServiceProvider;
                try
                {
                    var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
                    var userManager = provider.GetRequiredService<UserManager<AppUser>>();
                    var adminUser = await userManager.FindByEmailAsync("JhonAdmin@yopmail.com");
                    bool isExist = await roleManager.RoleExistsAsync(Roles.Archon);

                    if (!isExist && adminUser != null)
                    {
                        AppRole role = new AppRole()
                        {
                            Name = Roles.Archon,
                            Desc = "Admin role",
                            CreatedAt = DateTime.UtcNow,
                            CreatedById = adminUser.Id,
                            IsActive = true,
                        };
                        await roleManager.CreateAsync(role);
                    }
                }
                catch { throw; }
            }
        }
        public static async Task AssignAdminRoleToAdminIfNot(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateAsyncScope())
            {
                var provider = scope.ServiceProvider;
                try
                {
                    var roleManager = provider.GetRequiredService<RoleManager<AppRole>>();
                    var userManager = provider.GetRequiredService<UserManager<AppUser>>();

                    var adminUser = await userManager.FindByEmailAsync("JhonAdmin@yopmail.com");
                    if (adminUser is not null)
                    {
                        bool isAssigned = await userManager.IsInRoleAsync(adminUser, Roles.Archon);
                        if (isAssigned is false)
                        {
                            bool roleExist = await roleManager.RoleExistsAsync(Roles.Archon);
                            if (roleExist is true)
                            {
                                await userManager.AddToRoleAsync(adminUser, Roles.Archon);
                            }
                        }
                    }
                }
                catch { throw; }
            }
        }
    }
}
