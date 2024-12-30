using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.PostStatestic;

namespace MTAdmin.Infra.Data.Configs
{
    public class PostStatesticConfig : IEntityTypeConfiguration<PostStatistic>
    {
        public void Configure(EntityTypeBuilder<PostStatistic> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Identity).HasMaxLength(500);
            builder.Property(x => x.IPAddress).HasMaxLength(50);
        }
    }
}
