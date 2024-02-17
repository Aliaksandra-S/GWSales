using GWSales.Services.Models;
using GWSales.WebApi.Models.Storage;

namespace GWSales.Services.Interfaces;

public interface IStorageService
{
    Task<CommandResult<ResultType, GetSizeDto>> CreateSizeAsync(CreateSizeDto createSizeDto);
    Task<CommandResult<ResultType, GetProductSizeShortDto>> AddProductSizeAsync(AddProductSizeDto addProductSizeDto);
    Task<CommandResult<ResultType, ProductSizesListDto>> GetSizesByProductIdAsync(int productId);
    
    //Task<CommandResult<ResultType, CreateProductDto>> AddProductAsync(CreateProductDto productDto);
    //Task<CommandResult<ResultType, UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto);
}
