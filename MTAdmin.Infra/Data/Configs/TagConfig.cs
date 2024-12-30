using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Tag;

namespace MTAdmin.Infra.Data.Configs
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    HasMaxLength(100).
                    IsRequired();

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
