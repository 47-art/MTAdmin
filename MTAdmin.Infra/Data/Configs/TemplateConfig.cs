using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Template;

namespace MTAdmin.Infra.Data.Configs
{
    public class TemplateConfig : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    HasMaxLength(300).
                    IsRequired();

            builder.Property(x => x.Desc).
                    HasMaxLength(500).
                    IsRequired();

            builder.Property(x => x.CreatedAt).
                    IsRequired();

            builder.Property(x => x.CreatedBy).
                    HasMaxLength(455).
                    IsRequired();

            builder.Property(x => x.ModifiedBy).
                    HasMaxLength(455);

            builder.Property(x => x.FileName).
                    HasMaxLength(500).
                    IsRequired();

            builder.Property(x => x.Thumbnail).
                    HasMaxLength(500).
                    IsRequired();

            builder.Property(x => x.ThumbnailAlt).
                    HasMaxLength(500).
                    IsRequired();

            builder.HasOne(x => x.Language).
                    WithMany(x => x.Templates).
                    HasForeignKey(x => x.LanguageId).
                    OnDelete(DeleteBehavior.Restrict).
                    IsRequired(false);
        }
    }
}
