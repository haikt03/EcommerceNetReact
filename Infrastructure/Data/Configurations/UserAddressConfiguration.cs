using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration: BaseConfiguration<UserAddress>
    {
        public override void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.City).IsRequired();
            builder.Property(a => a.District).IsRequired();
            builder.Property(a => a.Ward).IsRequired();
            builder.Property(a => a.Street).IsRequired();
            builder.Property(a => a.Alley).IsRequired();
            builder.Property(a => a.HouseNumber).IsRequired();
        }
    }
}
