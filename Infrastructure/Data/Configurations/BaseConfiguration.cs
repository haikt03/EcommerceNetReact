using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(be => be.Id);
            builder.Property(be => be.Id).ValueGeneratedOnAdd();
            builder.Property(be => be.CreatedAt).IsRequired();
            builder.Property(be => be.ModifiedAt).IsRequired(false);
        }
    }
}
