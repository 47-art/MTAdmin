using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppRoleClaimConfig : IEntityTypeConfiguration<AppRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
        {
            builder.ToTable(name: "AppRoleClaim");

            builder.Property(x => x.Id).
                    HasColumnOrder(1);

            builder.Property(x => x.RoleId).
                    HasColumnOrder(2);

            builder.Property(x => x.ClaimType).
                    HasColumnOrder(3);

            builder.Property(x => x.ClaimValue).
                    HasColumnOrder(4);

            builder.HasOne(x => x.Role).
                    WithMany(x => x.RoleClaims).
                    HasForeignKey(x => x.RoleId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
