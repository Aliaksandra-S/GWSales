using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class StorageController : ControllerBase
{
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpPost]
    [Route("CreateSize")]
    public async Task<IActionResult> CreateSize([FromBody] CreateSizeDto sizeDto)
    {
        var result = await _storageService.CreateSizeAsync(sizeDto);

        if (result.ResultType == ResultType.ValidationError)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("AddProdSize")]
    public async Task<IActionResult> AddProductSize([FromBody] AddProductSizeDto productSizeDto)
    {
        var result = await _storageService.AddProductSizeAsync(productSizeDto);

        return result.ResultType switch
        {
            ResultType.NotFound => NotFound(result),
            ResultType.Failed => BadRequest(result),
            ResultType.ValidationError => BadRequest(result),
            _ => Ok(result),
        };
    }

    [HttpPost]
    [Route("GetSizesByProductId")]
    public async Task<IActionResult> GetSizesByProductId([FromBody] int productId)
    {
        var result = await _storageService.GetSizesByProductIdAsync(productId);

        return result.ResultType switch
        {
            ResultType.NotFound => NotFound(result),
            ResultType.Failed => BadRequest(result),
            ResultType.ValidationError => BadRequest(result),
            _ => Ok(result),
        };
    }

}
