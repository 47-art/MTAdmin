using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MTAdmin.Infra.Identity.Entities;
using System.Security.Claims;

namespace MTAdmin.Web.Areas.Archone.Services
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;
        public AppClaimsPrincipalFactory(UserManager<AppUser> userManager, 
                                         RoleManager<AppRole> roleManager, 
                                         IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);

            IList<string> roles = await _userManager.GetRolesAsync(user);
            string userRoles = string.Join(",", roles);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role,userRoles),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            });

            return principal;
        }
    }
}
