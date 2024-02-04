
using GWSales.Services.Models;
using GWSales.WebApi.Models.ProductAssortment;

namespace GWSales.Services;

public interface IProductService
{
    Task<CommandResult<ResultType, ProductListDto>> GetAllProductsAsync();
    Task<CommandResult<ResultType, AddProductDto>> AddProductAsync(AddProductDto productDto);
    Task<CommandResult<ResultType, UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto);

}
