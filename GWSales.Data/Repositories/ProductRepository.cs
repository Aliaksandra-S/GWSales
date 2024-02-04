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
        var models = await _context.Products.Select(x => new GetProductModel
        {
            ProductId = x.ProductId,
            ArticleNumber = x.ArticleNumber,
            ProductName = x.ProductName,
            WholesalePrice = x.WholesalePrice,
            RetailPrice = x.RetailPrice,
            UnitsInStock = x.UnitsInStock,
        }).ToListAsync();

        return new GetProductListModel
        {
            Products = models,
        };
    }

    /*
    public async Task<GetProductModel?> GetByArticleNumber(string article)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == article);

        if (entity == null)
        {
            return null;
        }

        return new GetProductModel
        {
            ArticleNumber = entity.ArticleNumber,
            ProductName = entity.ProductName,
            WholesalePrice = entity.WholesalePrice,
            RetailPrice = entity.RetailPrice,
            UnitsInStock = entity.UnitsInStock,
        };
    }
    */

    public async Task<GetProductModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

        if (entity == null)
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
            UnitsInStock = entity.UnitsInStock,
        };
    }
    public async Task<ProductEntity?> UpdateAsync(UpdateProductModel model)
    {
        var entity = _context.Products.FirstOrDefault(x => x.ProductId == model.ProductId);

        if (entity == null)
        {
            return null;
        }

        _context.Products.Attach(entity);

        entity.ProductName = model.ProductName;
        entity.WholesalePrice = model.WholesalePrice;
        entity.RetailPrice = model.RetailPrice;

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

        _context.Entry(entity).State = EntityState.Deleted;
        return await _context.SaveChangesAsync();
    }
   
}
