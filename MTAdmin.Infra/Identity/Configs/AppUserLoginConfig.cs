using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppUserLoginConfig : IEntityTypeConfiguration<AppUserLogin>
    {
        public void Configure(EntityTypeBuilder<AppUserLogin> builder)
        {
            builder.ToTable(name: "AppUserLogins");

            builder.Property(x => x.LoginProvider).
                    HasColumnOrder(1);

            builder.Property(x => x.ProviderKey).
                    HasColumnOrder(2);

            builder.Property(x => x.ProviderDisplayName).
                    HasColumnOrder(3).
                    HasMaxLength(100);

            builder.Property(x => x.UserId).
                    HasColumnOrder(4);

            builder.HasOne(x => x.User).
                    WithMany(x => x.UserLogins).
                    HasForeignKey(x => x.UserId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
