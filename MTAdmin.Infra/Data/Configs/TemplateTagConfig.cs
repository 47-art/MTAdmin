using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.TemplateTag;

namespace MTAdmin.Infra.Data.Configs
{
    public class TemplateTagConfig : IEntityTypeConfiguration<TemplateTag>
    {
        public void Configure(EntityTypeBuilder<TemplateTag> builder)
        {
            builder.HasKey(x => new { x.TemplateId, x.TagId });
            builder.HasOne(x => x.Template).
                    WithMany(x => x.TemplateTags).
                    HasForeignKey(x => x.TemplateId).
                    OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tag).
                    WithMany(x => x.TemplateTags).
                    HasForeignKey(x => x.TagId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
