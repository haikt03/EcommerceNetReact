using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.ModifiedAt).IsRequired(false);
            builder.OwnsOne(a => a.Image);

            builder
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<UserAddress>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
