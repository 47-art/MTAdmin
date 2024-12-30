using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTAdmin.Core.Entities.Contact;

namespace MTAdmin.Infra.Data.Configs
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).
                    HasMaxLength(300).
                    IsRequired();

            builder.Property(x => x.Email).
                    HasMaxLength(300).
                    IsRequired();

            builder.Property(x => x.Reason).
                    HasMaxLength(500);

            builder.Property(x => x.Message).
                    HasMaxLength(1000);

            builder.Property(x => x.CreatedAt).
                    IsRequired();
            builder.Property(x => x.IPAddress).
                    IsRequired().
                    HasMaxLength(50);
        }
    }
}
