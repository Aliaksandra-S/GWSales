using AutoMapper;
using GWSales.Data.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Product;
using GWSales.WebApi.Models.ProductAssortment;

namespace GWSales.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<CommandResult<ResultType, ProductListDto>> GetAllProductsAsync()
    {
        var result = new CommandResult<ResultType, ProductListDto>();
        var productListModel = await _productRepository.GetAllAsync();

        if (productListModel == null
            || productListModel.Products.Count() == 0)
        {
            result.ResultType = ResultType.Success;
            result.Messages?.Add("list is empty");
            return result;
        }

        var productDtos = _mapper.Map<ProductListDto>(productListModel);

        result.ResultType = ResultType.Success;
        result.Value = productDtos;

        return result;
    }

    public async Task<CommandResult<ResultType, GetProductDto>> AddProductAsync(CreateProductDto productDto)
    {
        var result = new CommandResult<ResultType, GetProductDto>();

        if (string.IsNullOrWhiteSpace(productDto.ArticleNumber)
            || string.IsNullOrWhiteSpace(productDto.ProductName))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Article number or product name is not valid.");
            return result;
        }

        if (productDto.WholesalePrice <= 0
            || productDto.RetailPrice <= 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Price can't be 0 or less than 0.");
            return result;
        }

        var createProductModel = _mapper.Map<CreateProductModel>(productDto);

        var entity = await _productRepository.CreateAsync(createProductModel);
        
        if (entity == null)
        {
            result.ResultType = ResultType.Failed;
            return result;
        }

        result.ResultType = ResultType.Success;
        result.Value = _mapper.Map<GetProductDto>(entity);
        return result;
    }

    public async Task<CommandResult<ResultType, UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto)
    {
        var result = new CommandResult<ResultType, UpdateProductDto>();

        if (productDto.ProductName != null && string.IsNullOrWhiteSpace(productDto.ProductName))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Product name is not valid.");
            return result;
        }

        if ((productDto.WholesalePrice != null && productDto.WholesalePrice <= 0)
            || (productDto.RetailPrice != null && productDto.RetailPrice <= 0))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages?.Add("Price can't be 0 or less than 0.");
            return result;
        }

        var model = _mapper.Map<UpdateProductModel>(productDto);

        var entity = await _productRepository.UpdateAsync(model);

        if (entity == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages?.Add("Product doesn't exist");
            return result;
        }    

        result.ResultType = ResultType.Success;
        result.Value = productDto;

        return result;
    }
}
