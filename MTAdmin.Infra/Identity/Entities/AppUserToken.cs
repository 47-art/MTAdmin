using Microsoft.AspNetCore.Identity;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppUserToken : IdentityUserToken<string>
    {
        public virtual AppUser User { get; set; }
    }
}
