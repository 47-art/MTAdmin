using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.SiteRequest;

namespace MTAdmin.Infra.Data.Configs
{
    public class SiteRequestConfig : IEntityTypeConfiguration<SiteRequest>
    {
        public void Configure(EntityTypeBuilder<SiteRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Identity).HasMaxLength(455);
            builder.Property(x => x.RemoteIpAddress).HasMaxLength(50);
            builder.Property(x => x.Method).HasMaxLength(16);
            builder.Property(x => x.UserAgent).HasMaxLength(512);
            builder.Property(x => x.Path).HasMaxLength(1024);
            builder.Property(x => x.Referer).HasMaxLength(1024);
        }
    }
}
