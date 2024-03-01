using GWSales.Data.Entities.Customer;
using GWSales.Data.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Extensions;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
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
