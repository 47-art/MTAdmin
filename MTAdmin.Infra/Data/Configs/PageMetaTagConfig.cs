using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.PageMetaTags;

namespace MTAdmin.Infra.Data.Configs
{
    public class PageMetaTagConfig : IEntityTypeConfiguration<PageMetaTag>
    {
        public void Configure(EntityTypeBuilder<PageMetaTag> builder)
        {
            builder.HasIndex(x => x.PageId).IsUnique();
            builder.Property(x => x.MetaTitle).HasMaxLength(100);
        }
    }
}
