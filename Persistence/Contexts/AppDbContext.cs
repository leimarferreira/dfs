using Microsoft.EntityFrameworkCore;
using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>().ToTable("Companies");
            builder.Entity<Company>().HasKey(p => p.Id);
            builder.Entity<Company>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Company>().Property(p => p.TradeName).IsRequired().HasMaxLength(70);
            builder.Entity<Company>().Property(p => p.LegalName).IsRequired().HasMaxLength(70);
            builder.Entity<Company>().Property(p => p.Cnpj).IsRequired().HasMaxLength(14);
            builder.Entity<Company>().HasMany(p => p.Products).WithOne(p => p.Company).HasForeignKey(p => p.Company.Id);

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(100);
            builder.Entity<Product>().Property(p => p.Value).IsRequired();
            builder.Entity<Product>().Property(p => p.Note).HasMaxLength(100);
            builder.Entity<Product>().Property(p => p.Company).IsRequired();

            builder.Entity<Purchase>().ToTable("Purchases");
            builder.Entity<Purchase>().HasKey(p => p.Id);
            builder.Entity<Purchase>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<Purchase>().Property(p => p.Value).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Date).IsRequired();
            builder.Entity<Purchase>().Property(p => p.PaymentMethod).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Status).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Note).HasMaxLength(100);
            builder.Entity<Purchase>().Property(p => p.PostalCode).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Address).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Product).IsRequired();
            builder.Entity<Purchase>().Property(p => p.Buyer).IsRequired();

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Name).IsRequired();
            builder.Entity<User>().Property(p => p.Email).IsRequired();
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.Cpf).IsRequired();
        }
    }
}
