using GWSales.Services;
using Microsoft.AspNetCore.Mvc;

namespace GWSales.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductAssortmentController : ControllerBase
    {
        private readonly ILogger<ProductAssortmentController> _logger;
        private readonly IProductService _productService;

        public ProductAssortmentController(ILogger<ProductAssortmentController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        // todo: AddProduct
         

        [HttpPost]
        public async Task<IActionResult> GetAllProducts()
        {
            var productsList = await _productService.GetAllProductsAsync();
            return Ok(productsList);
        }
    }
}