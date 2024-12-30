using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.UserAudit;

namespace MTAdmin.Infra.Identity.Configs
{
    public class UserAuditConfig : IEntityTypeConfiguration<UserAudit>
    {
        public void Configure(EntityTypeBuilder<UserAudit> builder)
        {
            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.IPAddress).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Event).IsRequired(true);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
