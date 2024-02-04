using GWSales.Data.Interfaces;
using GWSales.Services.Models;
using GWSales.Services.Models.Product;
using GWSales.WebApi.Models.ProductAssortment;

namespace GWSales.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepo)
    {
        _productRepository = productRepo;
    }

    public async Task<CommandResult<ResultType, ProductListDto>> GetAllProductsAsync()
    {
        var result = new CommandResult<ResultType, ProductListDto>();
        var productListModel = await _productRepository.GetAllAsync();

        if (productListModel == null
            || productListModel.Products.Count() == 0)
        {
            result.ResultType = ResultType.Success;
            result.Messages.Add("list is empty");
            return result;
        }

        var productDtos = productListModel.Products.Select(x => new GetProductDto
        {
            ProductId = x.ProductId,
            ArticleNumber = x.ArticleNumber,
            ProductName = x.ProductName,
            WholesalePrice = x.WholesalePrice,
            RetailPrice = x.RetailPrice,
            UnitsInStock = x.UnitsInStock,
        });

        result.ResultType = ResultType.Success;
        result.Value  = new ProductListDto
        {
            Products = productDtos,
        };

        return result;
    }

    public async Task<CommandResult<ResultType, AddProductDto>> AddProductAsync(AddProductDto productDto)
    {
        var result = new CommandResult<ResultType, AddProductDto>();

        if (string.IsNullOrWhiteSpace(productDto.ArticleNumber)
            || string.IsNullOrWhiteSpace(productDto.ProductName))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages.Add("Article number or product name is not valid.");
            return result;
        }

        if (productDto.WholesalePrice <= 0
            || productDto.RetailPrice <= 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages.Add("Price can't be 0 or less than 0.");
            return result;
        }

        var createProductModel = new CreateProductModel
        {
            ArticleNumber = productDto.ArticleNumber,
            ProductName = productDto.ProductName,
            WholesalePrice = productDto.WholesalePrice,
            RetailPrice = productDto.RetailPrice,
        };

        var entity = await _productRepository.CreateAsync(createProductModel);
        
        if (entity == null)
        {
            result.ResultType = ResultType.Failed;
            return result;
        }

        result.ResultType = ResultType.Created;
        result.Value = productDto;
        return result;
    }

    public async Task<CommandResult<ResultType, UpdateProductDto>> UpdateProductAsync(UpdateProductDto productDto)
    {
        var result = new CommandResult<ResultType, UpdateProductDto>();

        if (string.IsNullOrWhiteSpace(productDto.ProductName))
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages.Add("Product name is not valid.");
            return result;
        }

        if (productDto.WholesalePrice <= 0
            || productDto.RetailPrice <= 0)
        {
            result.ResultType = ResultType.ValidationError;
            result.Messages.Add("Price can't be 0 or less than 0.");
            return result;
        }

        var model = new UpdateProductModel
        {
            ProductId = productDto.ProductId,
            ProductName = productDto.ProductName,
            WholesalePrice = productDto.WholesalePrice,
            RetailPrice = productDto.RetailPrice,
        };

        var entity = await _productRepository.UpdateAsync(model);

        if (entity == null)
        {
            result.ResultType = ResultType.NotFound;
            result.Messages.Add("id is not found");
            return result;
        }    

        result.ResultType = ResultType.Success;
        result.Value = productDto;

        return result;
    }
}
