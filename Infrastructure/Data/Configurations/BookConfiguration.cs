using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class BookConfiguration : BaseConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Publisher).IsRequired();
            builder.Property(b => b.PublishedYear).IsRequired();
            builder.Property(b => b.Supplier).IsRequired();
            builder.Property(b => b.Language).IsRequired();
            builder.Property(b => b.Translator).IsRequired();
            builder.Property(b => b.ISBN).IsRequired();
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.QuantityInStock).IsRequired();

            builder
                    .HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
