using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core.Domain.Models;

namespace core.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.TradeName).IsRequired().HasMaxLength(70);
            builder.Property(p => p.LegalName).IsRequired().HasMaxLength(70);
            builder.Property(p => p.Cnpj).IsRequired().HasMaxLength(14);
            builder.Property(p => p.ImageDataURL);
        }
    }
}