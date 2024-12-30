using Microsoft.AspNetCore.Identity;
using MTAdmin.Infra.Identity.Entities.Comman;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppRole : IdentityRole, IBaseEntity
    {
        public string Desc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedById { get; set; }
        public AppUser ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppRoleClaim> RoleClaims { get; set; }
    }
}
