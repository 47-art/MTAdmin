using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable(name: "AppRoles");

            builder.Property(x => x.Id).
                    HasColumnOrder(1);

            builder.Property(x => x.Name).
                    HasColumnOrder(2).
                    HasMaxLength(100);

            builder.Property(x => x.NormalizedName).
                    HasColumnOrder(3).
                    HasMaxLength(100);

            builder.Property(x => x.ConcurrencyStamp).
                    HasColumnOrder(4);

            builder.Property(x => x.Desc).
                    HasColumnOrder(5).
                    HasMaxLength(500);

            builder.Property(x => x.CreatedAt).
                    HasColumnOrder(6).
                    IsRequired();

            builder.Property(x => x.CreatedById).
                    HasColumnOrder(7).
                    IsRequired();

            builder.Property(x => x.ModifiedAt).
                    HasColumnOrder(8);

            builder.Property(x => x.ModifiedById).
                    HasColumnOrder(9);

            builder.Property(x => x.IsActive).
                    HasColumnOrder(10);

            builder.Property(x => x.IsDeleted).
                    HasColumnOrder(11);
        }
    }
}
