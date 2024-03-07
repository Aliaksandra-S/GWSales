using GWSales.Data.Entities.Customer;
using GWSales.Data.Entities.Order;
using GWSales.Data.Entities.Product;
using GWSales.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data;

public class SalesDbContext : IdentityDbContext<UserEntity>
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
    public DbSet<DiscountProgramEntity> CustomerDiscounts { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailsEntity> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
            .HasOne(c => c.CustomerType)
            .WithMany(ct => ct.Customers)
            .HasForeignKey(c => c.CustomerTypeId);

        modelBuilder.Entity<CustomerEntity>()
            .HasOne(cd => cd.Discount)
            .WithMany(c => c.Customers)
            .HasForeignKey(cd => cd.DiscountId);

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

        modelBuilder.Entity<CustomerTypeEntity>()
            .HasIndex(p => p.TypeName)
            .IsUnique();

        SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerTypeEntity>()
            .HasData(new CustomerTypeEntity
            {
                CustomerTypeId = 1,
                TypeName = "Wholesale",
            },
            new CustomerTypeEntity
            {
                CustomerTypeId = 2,
                TypeName = "Retail",
            });

        modelBuilder.Entity<SizeEntity>()
            .HasData(
            new SizeEntity { SizeId = 1, SizeRuName = "42" },
            new SizeEntity { SizeId = 2, SizeRuName = "44" },
            new SizeEntity { SizeId = 3, SizeRuName = "46" },
            new SizeEntity { SizeId = 4, SizeRuName = "48" },
            new SizeEntity { SizeId = 5, SizeRuName = "50" },
            new SizeEntity { SizeId = 6, SizeRuName = "52" },
            new SizeEntity { SizeId = 7, SizeRuName = "54" },
            new SizeEntity { SizeId = 8, SizeRuName = "56" }
            );
    }
}
