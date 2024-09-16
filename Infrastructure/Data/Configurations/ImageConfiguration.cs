using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ImageConfiguration : BaseConfiguration<Image>
    {
        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.Url).IsRequired();
            builder.Property(i => i.PublicId).IsRequired();
        }
    }
}
