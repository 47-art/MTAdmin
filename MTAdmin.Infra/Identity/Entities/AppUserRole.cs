using Microsoft.AspNetCore.Identity;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual AppRole Role { get; set; }
    }
}
