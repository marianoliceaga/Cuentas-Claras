using Microsoft.AspNetCore.Mvc;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("by-subcategory/{subcategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsBySubcategory(int subcategoryId)
        {
            var products = await _productService.GetProductsBySubcategoryAsync(subcategoryId);
            return Ok(products);
        }

        [HttpGet("{id}/exists")]
        public async Task<ActionResult<bool>> ProductExists(int id)
        {
            var exists = await _productService.ProductExistsAsync(id);
            return Ok(exists);
        }
    }
}
