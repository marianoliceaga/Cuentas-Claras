using Microsoft.AspNetCore.Mvc;
using CuentasClaras.API.Services.Interfaces;
using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Controllers
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

        /// <summary>
        /// Get all products with pagination and optional filtering
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductDto>>>> GetAllProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? subcategoryId = null)
        {
            try
            {
                var result = await _productService.GetAllAsync(pageNumber, pageSize, searchTerm, subcategoryId);
                return Ok(ApiResponse<PagedResult<ProductDto>>.SuccessResult(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PagedResult<ProductDto>>.ErrorResult($"Error retrieving products: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get a specific product by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound(ApiResponse<ProductDto>.ErrorResult("Product not found"));
                }

                return Ok(ApiResponse<ProductDto>.SuccessResult(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductDto>.ErrorResult($"Error retrieving product: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDto>>> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<ProductDto>.ErrorResult("Validation errors", errors));
                }

                var product = await _productService.CreateAsync(createProductDto);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ApiResponse<ProductDto>.SuccessResult(product, "Product created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductDto>.ErrorResult($"Error creating product: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update an existing product
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDto>>> UpdateProduct(int id, [FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<ProductDto>.ErrorResult("Validation errors", errors));
                }

                var product = await _productService.UpdateAsync(id, updateProductDto);
                if (product == null)
                {
                    return NotFound(ApiResponse<ProductDto>.ErrorResult("Product not found"));
                }

                return Ok(ApiResponse<ProductDto>.SuccessResult(product, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductDto>.ErrorResult($"Error updating product: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(ApiResponse<bool>.ErrorResult("Product not found"));
                }

                return Ok(ApiResponse<bool>.SuccessResult(true, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.ErrorResult($"Error deleting product: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get all subcategories
        /// </summary>
        [HttpGet("subcategories")]
        public async Task<ActionResult<ApiResponse<List<SubcategoryDto>>>> GetSubcategories()
        {
            try
            {
                var subcategories = await _productService.GetSubcategoriesAsync();
                return Ok(ApiResponse<List<SubcategoryDto>>.SuccessResult(subcategories));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<SubcategoryDto>>.ErrorResult($"Error retrieving subcategories: {ex.Message}"));
            }
        }
    }
}
