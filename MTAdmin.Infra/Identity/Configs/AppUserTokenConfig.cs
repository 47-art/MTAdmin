using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppUserTokenConfig : IEntityTypeConfiguration<AppUserToken>
    {
        public void Configure(EntityTypeBuilder<AppUserToken> builder)
        {
            builder.ToTable(name: "AppUserTokens");

            builder.Property(x => x.UserId).
                    HasColumnOrder(1);

            builder.Property(x => x.LoginProvider).
                    HasColumnOrder(2);

            builder.Property(x => x.Name).
                    HasColumnOrder(3);

            builder.Property(x => x.Value).
                    HasColumnOrder(4);

            builder.HasOne(x => x.User).
                    WithMany(x => x.UserTokens).
                    HasForeignKey(x => x.UserId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
