using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppUserRoleConfig : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.ToTable(name: "AppUserRoles");

            builder.Property(x => x.UserId).
                    HasColumnOrder(1);

            builder.Property(x => x.RoleId).
                    HasColumnOrder(2);

            builder.HasOne(x => x.User).
                    WithMany(x => x.UserRoles).
                    HasForeignKey(x => x.UserId).
                    OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Role).
                    WithMany(x => x.UserRoles).
                    HasForeignKey(x => x.RoleId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
