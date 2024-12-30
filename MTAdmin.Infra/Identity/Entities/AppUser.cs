using Microsoft.AspNetCore.Identity;
using MTAdmin.Infra.Identity.Entities.Comman;

namespace MTAdmin.Infra.Identity.Entities
{
    public class AppUser : IdentityUser, IBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public string ProfileImg { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedById { get; set; }
        public AppUser ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual ICollection<AppUserClaim> UserClaims { get; set; }
        public virtual ICollection<AppUserToken> UserTokens { get; set; }
        public virtual ICollection<AppUserLogin> UserLogins { get; set; }
    }
}
