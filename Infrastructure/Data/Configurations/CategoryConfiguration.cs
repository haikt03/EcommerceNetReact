using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : BaseConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired();

            builder
                .HasOne(c => c.PCategory)
                .WithMany(c => c.CCategories)
                .HasForeignKey(c => c.PId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
