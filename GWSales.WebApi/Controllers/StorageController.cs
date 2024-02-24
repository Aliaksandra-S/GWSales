using GWSales.Services.Interfaces;
using GWSales.Services.Models;
using GWSales.WebApi.Models.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageController : ControllerBase
{
    private readonly ILogger<StorageController> _logger;
    private readonly IStorageService _storageService;

    public StorageController(ILogger<StorageController> logger, IStorageService storageService)
    {
        _logger = logger;
        _storageService = storageService;
    }

    [HttpPost]
    [Route("Create")]
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
    [Route("Add")]
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
    [Route("GetSizes")]
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
