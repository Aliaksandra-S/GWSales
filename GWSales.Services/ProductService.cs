using GWSales.Data.Interfaces;
using GWSales.WebApi.Models.ProductAssortment;
using Microsoft.EntityFrameworkCore;

namespace GWSales.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<ProductListDto> GetAllProductsAsync()
    {
        var products = await _productRepo.GetAll().ToListAsync();

        return new ProductListDto
        {
            Products = products.Select(x => new ProductDto
            {
                ArticleNumber = x.ArticleNumber,
                ProductName = x.ProductName,
                WholesalePrice = x.WholesalePrice,
                RetailPrice = x.RetailPrice,
                UnitsInStock = x.UnitsInStock
            }).ToList(),
        };
    }
}
