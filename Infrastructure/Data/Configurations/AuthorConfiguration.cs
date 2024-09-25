using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : BaseConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.Biography).IsRequired();
            builder.Property(a => a.Country).IsRequired();
            builder.OwnsOne(a => a.Image);
        }
    }
}
