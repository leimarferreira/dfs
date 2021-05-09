using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Persistence.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.PaymentMethod).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Note).HasMaxLength(100);
            builder.Property(p => p.PostalCode).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.HasOne(p => p.Product).WithMany().IsRequired();
            builder.HasOne(p => p.Buyer).WithMany(p => p.Purchases).IsRequired();
        }
    }
}