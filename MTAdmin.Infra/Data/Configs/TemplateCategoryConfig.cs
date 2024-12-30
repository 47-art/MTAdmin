using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.TemplateCategory;

namespace MTAdmin.Infra.Data.Configs
{
    public class TemplateCategoryConfig : IEntityTypeConfiguration<TemplateCategory>
    {
        public void Configure(EntityTypeBuilder<TemplateCategory> builder)
        {
            builder.HasKey(x => new { x.TemplateId, x.CategoryId });
            builder.HasOne(x => x.Template).
                    WithMany(x => x.TemplateCategories).
                    HasForeignKey(x => x.TemplateId).
                    OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category).
                    WithMany(x => x.TemplateCategories).
                    HasForeignKey(x => x.CategoryId).
                    OnDelete(DeleteBehavior.Restrict);
        }
    }
}
