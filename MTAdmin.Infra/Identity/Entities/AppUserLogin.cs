using Microsoft.AspNetCore.Identity;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppUserLogin : IdentityUserLogin<string>
    {
        public virtual AppUser User { get; set; }
    }
}
