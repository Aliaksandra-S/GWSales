using GWSales.Services;
using GWSales.Services.Models;
using GWSales.WebApi.Models.ProductAssortment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class ProductAssortmentController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductAssortmentController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductDto productDto)
    {
        var result = await _productService.AddProductAsync(productDto);
        
        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("Edit")]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto productDto)
    {
        var result = await _productService.UpdateProductAsync(productDto);

        if(result.ResultType == ResultType.NotFound)
        {
            return NotFound(result);
        }

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }
        return Ok(result);

    }

    [HttpPost]
    [Route("GetAll")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _productService.GetAllProductsAsync();
        return Ok(result);
    }


}
