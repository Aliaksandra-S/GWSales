using GWSales.Data.Entities.Product;
using GWSales.Data.Interfaces;
using GWSales.Services.Models.Storage;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Data.Repositories;

public class ProductSizeRepository: IProductSizeRepository
{
    private readonly SalesDbContext _context;

    public ProductSizeRepository(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductSizeListModel?> GetSizesByProductIdAsync(int productId)
    {
        var productSizes = await _context.ProductSizes.Where(x => x.ProductId == productId)
            .Join(_context.Sizes,
            outer => outer.SizeId,
            inner => inner.SizeId,
            (outer, inner) => new GetProductSizeModel
            {
                ProductSizeId = outer.ProductSizeId,
                SizeId = inner.SizeId,
                SizeRuName = inner.SizeRuName,
                Quantity = outer.Quantity,
            }).ToListAsync();

        if (productSizes == null)
        {
            return null;
        }
        
        return new GetProductSizeListModel
        {
            ProductId = productId,
            ProductSizes = productSizes,
        };
    }

    public async Task<ProductSizeEntity?> AddProductSizeAsync(AddProductSizeModel productSizeModel)
    {
        var entity = new ProductSizeEntity
        {
            ProductId = productSizeModel.ProductId,
            SizeId = productSizeModel.SizeId,
            Quantity = productSizeModel.Quantity,
        };

        var resultEntity = await _context.ProductSizes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
    }

    public async Task<SizeEntity?> CreateSizeAsync(CreateSizeModel sizeModel)
    {
        var entity = new SizeEntity
        {
            SizeRuName = sizeModel.SizeRuName,
        };

        var resultEntity = await _context.Sizes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return resultEntity.Entity;
    }

    public async Task<SizeEntity?> GetByIdAsync(int sizeId)
    {
        return await _context.Sizes.FindAsync(sizeId);
    }

    public async Task<SizeEntity?> GetSizeByNameAsync(string name)
    {
        return await _context.Sizes.FindAsync(name);
    }

    public async Task<GetProductSizeModel?> GetProductSizeByProductAndSizeAsync(int productId, int sizeId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return null;
        }

        var size = await _context.Sizes.FindAsync(sizeId);
        if (size == null)
        {
            return null;
        }

        return await _context.ProductSizes.Where(x => x.ProductId == productId && x.SizeId == sizeId)
            .Select(x => new GetProductSizeModel
            {
                ProductSizeId = x.ProductSizeId,
                Quantity = x.Quantity,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> ChangeProductSizeQuantityAsync(ChangeProductSizeQuantityModel quantityModel)
    {
        var productSize = await _context.ProductSizes.FindAsync(quantityModel.ProductSizeId);
        if (productSize == null)
        {
            return -1;
        }

        productSize.Quantity = quantityModel.NewQuantity;

        return await _context.SaveChangesAsync();
    }
}
