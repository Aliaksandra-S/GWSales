using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Product;

namespace GWSales.Data.Interfaces;

public interface IProductRepository
{
    Task<ProductEntity> CreateAsync(CreateProductModel model);
    Task<GetProductListModel> GetAllAsync();
    Task<GetProductModel?> GetByIdAsync(int id);
    Task<GetProductPriceListModel> GetPricesByProductIdAsync(GetPriceByCustomerTypeListModel models);
    Task<ProductEntity?> UpdateAsync(UpdateProductModel model);
    Task<int> DeleteAsync(DeleteProductModel model);
}
