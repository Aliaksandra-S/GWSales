using GWSales.Data.Entities.Product;
using GWSales.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GWSales.Data.Repositories;

public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
{
    public ProductRepository(SalesDbContext context) : base(context)
    {
    }

    public Task<ProductEntity> GetByArticleAsync(string articleNumber)
    {
        return _dbContext.Products.FirstOrDefaultAsync(x => x.ArticleNumber == articleNumber);
    }
}
