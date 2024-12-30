using Microsoft.AspNetCore.Identity;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppUserClaim : IdentityUserClaim<string>
    {
        public virtual AppUser User { get; set; }
    }
}
