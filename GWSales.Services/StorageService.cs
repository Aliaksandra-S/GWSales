using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Extensions;
using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Storage;
using GWSales.WebApi.Models.Storage;

namespace GWSales.Services;

public class StorageService : IStorageService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductSizeRepository _sizeRepository;
    private readonly IMapper _mapper;

    public StorageService(IMapper mapper, IProductRepository productRepository, IProductSizeRepository sizeRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _sizeRepository = sizeRepository;
    }

    public async Task<CommandResult<ResultType, GetProductSizeShortDto>> AddProductSizeAsync(AddProductSizeDto addProductSizeDto)
    {
        var result = new CommandResult<ResultType, GetProductSizeShortDto>();

        if (await _sizeRepository.GetSizeByIdAsync(addProductSizeDto.SizeId) == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Size ID is not found.");

            return result;
        }

        if (await _productRepository.GetByIdAsync(addProductSizeDto.ProductId) == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Product ID is not found.");

            return result;
        }

        var model = _mapper.Map<AddProductSizeModel>(addProductSizeDto);
        
        var resultEntity = await _sizeRepository.AddProductSizeAsync(model);

        if (resultEntity == null)
        {
            result.ResultType = ResultType.Failed;
            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetProductSizeShortDto>(resultEntity);
        return result;
    }

    public async Task<CommandResult<ResultType, GetSizeDto>> CreateSizeAsync(CreateSizeDto createSizeDto)
    {
        var result = new CommandResult<ResultType, GetSizeDto>();

        if (!createSizeDto.SizeRuName.IsValidSize())
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Size name is not valid.");

            return result;
        }

        var model = _mapper.Map<CreateSizeModel>(createSizeDto);
        var entity = await _sizeRepository.CreateSizeAsync(model);

        if (entity == null)
        {
            result.ResultType = ResultType.Failed;
            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetSizeDto>(entity);
        return result;
    }

    public async Task<CommandResult<ResultType, ProductSizesListDto>> GetSizesByProductIdAsync(int productId)
    {
        //todo: productName не подтягивается
        var result = new CommandResult<ResultType, ProductSizesListDto>();

        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Product ID is not found.");
            return result;
        }

        var productSizesList = await _sizeRepository.GetByProductIdAsync(productId);

        var sizesDto = _mapper.Map<ProductSizesListDto>(productSizesList);
        sizesDto.ProductName = product.ProductName;
        sizesDto.TotalQuantity = sizesDto.ProductSizes.Select(x => x.Quantity).Sum();

        result.ResultType = ResultType.Success;
        result.Value = sizesDto;
        return result;
    }
}
