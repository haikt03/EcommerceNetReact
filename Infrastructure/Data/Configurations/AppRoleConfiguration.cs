using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder
                .HasData(
                    new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                    new AppRole { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
                );
        }
    }
}
