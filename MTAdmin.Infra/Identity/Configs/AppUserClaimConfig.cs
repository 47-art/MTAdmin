using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Infra.Identity.Entities;

namespace MTAdmin.Infra.Identity.Configs
{
    public class AppUserClaimConfig : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.ToTable(name: "AppUserClaims");

            builder.Property(x => x.Id).
                    HasColumnOrder(1);

            builder.Property(x => x.UserId).
                    HasColumnOrder(2);

            builder.Property(x => x.ClaimType).
                    HasColumnOrder(3);

            builder.Property(x => x.ClaimValue).
                    HasColumnOrder(4);

            builder.HasOne(x => x.User).
                    WithMany(x => x.UserClaims).
                    HasForeignKey(x => x.UserId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
