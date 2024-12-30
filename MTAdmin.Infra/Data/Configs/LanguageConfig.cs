using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Language;

namespace MTAdmin.Infra.Data.Configs
{
    public class LanguageConfig : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    HasMaxLength(250).
                    IsRequired();

            builder.Property(x => x.NativeName).
                    HasMaxLength(300);

            builder.Property(x => x.CreatedAt).
                    IsRequired();

            builder.Property(x => x.CreatedBy).
                    HasMaxLength(455).
                    IsRequired();

            builder.Property(x => x.ModifiedBy).
                    HasMaxLength(455);
        }
    }
}
