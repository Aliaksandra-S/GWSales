using GWSales.Data.Entities.Product;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SalesDbContext _context;

    public ProductRepository(SalesDbContext context)
    {
        _context = context;
    }
    public async Task<ProductEntity> CreateAsync(CreateProductModel model)
    {
        var entity = new ProductEntity
        {
            ArticleNumber = model.ArticleNumber,
            ProductName = model.ProductName,
            WholesalePrice = model.WholesalePrice,
            RetailPrice = model.RetailPrice,
        };

        var resultEntity = await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
 
    }

    public async Task<GetProductListModel> GetAllAsync()
    {
        var models = await _context.Products
            .Where(x => !x.IsDeleted)
            .Select(x => new GetProductModel
        {
            ProductId = x.ProductId,
            ArticleNumber = x.ArticleNumber,
            ProductName = x.ProductName,
            WholesalePrice = x.WholesalePrice,
            RetailPrice = x.RetailPrice,
        }).ToListAsync();

        return new GetProductListModel
        {
            Products = models,
        };
    }

    public async Task<GetProductModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

        if (entity == null || entity.IsDeleted)
        {
            return null;
        }

        return new GetProductModel
        {
            ProductId = entity.ProductId,
            ArticleNumber = entity.ArticleNumber,
            ProductName = entity.ProductName,
            WholesalePrice = entity.WholesalePrice,
            RetailPrice = entity.RetailPrice,
        };
    }

    public async Task<ProductEntity?> UpdateAsync(UpdateProductModel model)
    {
        var entity = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

        if (entity == null || entity.IsDeleted)
        {
            return null;
        }

        _context.Products.Attach(entity);

        if (model.ProductName != null)
        {
            entity.ProductName = model.ProductName;
        }

        if (model.WholesalePrice != null)
        {
            entity.WholesalePrice = (decimal)model.WholesalePrice;
        }

        if (model.RetailPrice != null)
        {
            entity.RetailPrice = (decimal)model.RetailPrice;
        }

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<int> DeleteAsync(DeleteProductModel model)
    {
        var entity = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

        if (entity == null)
        {
            return -1;
        }

        entity.IsDeleted = true;
        return await _context.SaveChangesAsync();
    }

    public async Task<GetProductPriceListModel> GetPricesByProductIdAsync(GetPriceByCustomerTypeListModel models)
    {
        var result = new GetProductPriceListModel();

        foreach (var model in models.ProductsWithCustomerTypes)
        {
            var price = await _context.Products
                .Where(x => x.ProductId == model.ProductId)
                .Select(x => new GetProductPriceModel
                {
                    ProductId = x.ProductId,
                    Price = model.CustomerTypeId == 1 ? x.WholesalePrice : x.RetailPrice,
                })
                .FirstOrDefaultAsync();

            if (price != null)
            {
                result.Prices.Add(price);
            }
        }

        return result;
    }
}
