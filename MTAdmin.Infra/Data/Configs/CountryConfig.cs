using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Country;

namespace MTAdmin.Infra.Data.Configs
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.OfficialName).HasMaxLength(150);
            builder.Property(x => x.Alpha2Code).HasMaxLength(2);
            builder.Property(x => x.Alpha3Code).HasMaxLength(4);
            builder.Property(x => x.TopLevelDomain).HasMaxLength(12);
        }
    }
}
