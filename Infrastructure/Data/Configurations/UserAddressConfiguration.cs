using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration: BaseConfiguration<UserAddress>
    {
        public override void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            base.Configure(builder);

            builder.Property(ua => ua.City).IsRequired();
            builder.Property(ua => ua.District).IsRequired();
            builder.Property(ua => ua.Ward).IsRequired();
            builder.Property(ua => ua.Street).IsRequired();
            builder.Property(ua => ua.Alley).IsRequired();
            builder.Property(ua => ua.HouseNumber).IsRequired();
        }
    }
}
