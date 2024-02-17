using GWSales.Data.Entities.Product;
using GWSales.Services.Models.Storage;

namespace GWSales.Data.Interfaces;

public interface IProductSizeRepository
{
    Task<GetProductSizeListModel?> GetByProductIdAsync(int productId);

    Task<ProductSizeEntity?> AddProductSizeAsync(AddProductSizeModel productSizeModel);

    Task<SizeEntity?> CreateSizeAsync(CreateSizeModel sizeModel);

    Task<SizeEntity?> GetSizeByIdAsync(int sizeId);
    Task<SizeEntity?> GetSizeByNameAsync(string name);

}
