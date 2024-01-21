
using GWSales.WebApi.Models.ProductAssortment;

namespace GWSales.Services;

public interface IProductService
{
   Task<ProductListDto> GetAllProductsAsync();
}
