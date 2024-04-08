using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Storage;

namespace GWSales.Data.Interfaces;

public interface IProductSizeRepository
{
    Task<GetProductSizeListModel?> GetSizesByProductIdAsync(int productId);

    Task<ProductSizeEntity?> AddProductSizeAsync(AddProductSizeModel productSizeModel);

    Task<GetProductSizeModel?> GetProductSizeByProductAndSizeAsync(int productId, int sizeId);

    Task<int> ChangeProductSizeQuantityAsync(ChangeProductSizeQuantityModel quantityModel);

    Task<SizeEntity?> CreateSizeAsync(CreateSizeModel sizeModel);

    Task<SizeEntity?> GetByIdAsync(int sizeId);

    Task<SizeEntity?> GetSizeByNameAsync(string name);

}
