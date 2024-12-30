using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.EmailSubscriber;

namespace MTAdmin.Infra.Data.Configs
{
    public class EmailSubscriberConfig : IEntityTypeConfiguration<EmailSubscriber>
    {
        public void Configure(EntityTypeBuilder<EmailSubscriber> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).
                    HasMaxLength(300).
                    IsRequired();
        }
    }
}
