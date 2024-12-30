using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable(name: "AppUsers");

            builder.Property(x => x.Id).
                    HasColumnOrder(1);

            builder.Property(x => x.FirstName).
                    HasColumnOrder(2).
                    HasMaxLength(100);

            builder.Property(x => x.LastName).
                    HasColumnOrder(3).
                    HasMaxLength(100);

            builder.Property(x => x.DOB).
                    HasColumnOrder(4);

            builder.Property(x => x.ProfileImg).
                    HasColumnOrder(5).
                    HasMaxLength(100);

            builder.Property(x => x.LastLoginAt).
                    HasColumnOrder(6);

            builder.Property(x => x.UserName).
                    HasColumnOrder(7);

            builder.Property(x => x.NormalizedUserName).
                    HasColumnOrder(8);

            builder.Property(x => x.Email).
                    HasColumnOrder(9);

            builder.Property(x => x.NormalizedEmail).
                    HasColumnOrder(10);

            builder.Property(x => x.EmailConfirmed).
                    HasColumnOrder(11);

            builder.Property(x => x.PasswordHash).
                    HasColumnOrder(12);

            builder.Property(x => x.SecurityStamp).
                    HasColumnOrder(13);

            builder.Property(x => x.ConcurrencyStamp).
                    HasColumnOrder(14);

            builder.Property(x => x.PhoneNumber).
                    HasColumnOrder(15);

            builder.Property(x => x.PhoneNumberConfirmed).
                    HasColumnOrder(16);

            builder.Property(x => x.TwoFactorEnabled).
                    HasColumnOrder(17);

            builder.Property(x => x.LockoutEnd).
                    HasColumnOrder(18);

            builder.Property(x => x.LockoutEnabled).
                    HasColumnOrder(19);

            builder.Property(x => x.AccessFailedCount).
                    HasColumnOrder(20);

            builder.Property(x => x.CreatedAt).
                    HasColumnOrder(21).
                    IsRequired();

            builder.Property(x => x.CreatedById).
                    HasColumnOrder(22).
                    IsRequired();

            builder.Property(x => x.ModifiedAt).
                    HasColumnOrder(23);

            builder.Property(x => x.ModifiedById).
                    HasColumnOrder(24);

            builder.Property(x => x.IsActive).
                    HasColumnOrder(25);

            builder.Property(x => x.IsDeleted).
                    HasColumnOrder(26);

            builder.HasOne(x => x.CreatedBy).
                    WithMany().
                    HasForeignKey(x => x.CreatedById).
                    OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(x => x.ModifiedBy).
                    WithMany().
                    HasForeignKey(x => x.ModifiedById).
                    OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
