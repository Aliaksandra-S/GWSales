using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Product;

namespace GWSales.Data.Interfaces;

public interface IProductRepository
{
    Task<ProductEntity> CreateAsync(CreateProductModel model);
    Task<GetProductListModel> GetAllAsync();
    Task<GetProductModel?> GetByIdAsync(int id);
    //Task<GetProductModel?> GetByArticleNumber(string article);
    Task<ProductEntity?> UpdateAsync(UpdateProductModel model);
    Task<int> DeleteAsync(DeleteProductModel model);
}
