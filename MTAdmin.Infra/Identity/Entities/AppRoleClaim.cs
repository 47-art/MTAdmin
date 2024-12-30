using Microsoft.AspNetCore.Identity;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppRoleClaim : IdentityRoleClaim<string>
    {
        public virtual AppRole Role { get; set; }
    }
}
