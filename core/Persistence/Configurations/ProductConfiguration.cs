using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.Domain.Models;

namespace core.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Note).HasMaxLength(100);
            builder.Property(p => p.ImageDataURL);
            builder.HasOne(p => p.Company)
                .WithMany(p => p.Products).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}