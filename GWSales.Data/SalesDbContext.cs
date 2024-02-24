using GWSales.Data.Entities.Customer;
using GWSales.Data.Entities.Order;
using GWSales.Data.Entities.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data;

public class SalesDbContext : IdentityDbContext<IdentityUser>
{
    public SalesDbContext()
    {

    }

    public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("ConnectionString");
        }

        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<SizeEntity> Sizes { get; set; }
    public DbSet<ProductSizeEntity> ProductSizes { get; set; } 
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<CustomerTypeEntity> CustomerTypes { get; set; }
    public DbSet<CustomerDiscountEntity> CustomerDiscounts { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailsEntity> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasOne(c => c.CustomerType)
            .WithMany(ct => ct.Customers)
            .HasForeignKey(c => c.CustomerTypeId);

        modelBuilder.Entity<CustomerDiscountEntity>()
            .HasOne(cd => cd.Customer)
            .WithMany(c => c.Discounts)
            .HasForeignKey(cd => cd.CustomerId);

        modelBuilder.Entity<ProductSizeEntity>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.Sizes)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder.Entity<ProductEntity>()
            .HasIndex(p => p.ArticleNumber)
            .IsUnique();

        modelBuilder.Entity<SizeEntity>()
            .HasIndex(p => p.SizeRuName)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
