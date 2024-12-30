using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Category;

namespace MTAdmin.Infra.Data.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    IsRequired();

            builder.Property(x => x.Desc).
                    HasMaxLength(400);

            builder.Property(x => x.Img).
                    HasMaxLength(255);

            builder.Property(x => x.ImgAlt).
                    HasMaxLength(255);

            builder.Property(x => x.CreatedBy).
                    HasMaxLength(455).
                    IsRequired();

            builder.Property(x => x.CreatedAt).
                    IsRequired();

            builder.Property(x => x.ModifiedBy).
                    HasMaxLength(455);
        }
    }
}
