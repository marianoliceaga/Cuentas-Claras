using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPlansController : ControllerBase
    {
        private readonly IProductPlanService _productPlanService;

        public ProductPlansController(IProductPlanService productPlanService)
        {
            _productPlanService = productPlanService;
        }

        /// <summary>
        /// Get all product-plan relationships with pagination and optional filtering
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<ProductPlanDto>>>> GetAllProductPlans(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? planId = null,
            [FromQuery] int? productId = null)
        {
            try
            {
                var result = await _productPlanService.GetAllAsync(pageNumber, pageSize, planId, productId);
                return Ok(ApiResponse<PagedResult<ProductPlanDto>>.SuccessResult(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PagedResult<ProductPlanDto>>.ErrorResult($"Error retrieving product plans: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get a specific product-plan relationship by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductPlanDto>>> GetProductPlan(int id)
        {
            try
            {
                var productPlan = await _productPlanService.GetByIdAsync(id);
                if (productPlan == null)
                {
                    return NotFound(ApiResponse<ProductPlanDto>.ErrorResult("Product plan not found"));
                }

                return Ok(ApiResponse<ProductPlanDto>.SuccessResult(productPlan));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult($"Error retrieving product plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get all products associated with a specific plan
        /// </summary>
        [HttpGet("plan/{planId}")]
        public async Task<ActionResult<ApiResponse<List<ProductPlanDto>>>> GetProductsByPlanId(int planId)
        {
            try
            {
                var productPlans = await _productPlanService.GetByPlanIdAsync(planId);
                return Ok(ApiResponse<List<ProductPlanDto>>.SuccessResult(productPlans));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<List<ProductPlanDto>>.ErrorResult($"Error retrieving products for plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create a new product-plan relationship
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductPlanDto>>> CreateProductPlan([FromBody] CreateProductPlanDto createProductPlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult("Validation errors", errors));
                }

                var productPlan = await _productPlanService.CreateAsync(createProductPlanDto);
                return CreatedAtAction(nameof(GetProductPlan), new { id = productPlan.Id }, ApiResponse<ProductPlanDto>.SuccessResult(productPlan, "Product plan created successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult($"Error creating product plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update an existing product-plan relationship
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProductPlanDto>>> UpdateProductPlan(int id, [FromBody] UpdateProductPlanDto updateProductPlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult("Validation errors", errors));
                }

                var productPlan = await _productPlanService.UpdateAsync(id, updateProductPlanDto);
                if (productPlan == null)
                {
                    return NotFound(ApiResponse<ProductPlanDto>.ErrorResult("Product plan not found"));
                }

                return Ok(ApiResponse<ProductPlanDto>.SuccessResult(productPlan, "Product plan updated successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<ProductPlanDto>.ErrorResult($"Error updating product plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete a product-plan relationship
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteProductPlan(int id)
        {
            try
            {
                var result = await _productPlanService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(ApiResponse<bool>.ErrorResult("Product plan not found"));
                }

                return Ok(ApiResponse<bool>.SuccessResult(true, "Product plan deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.ErrorResult($"Error deleting product plan: {ex.Message}"));
            }
        }

        [HttpGet("exists")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckProductPlanExists([FromQuery] int planId, [FromQuery] int productId)
        {
            try
            {
                var exists = await _productPlanService.ExistsAsync(planId, productId);
                return Ok(ApiResponse<bool>.SuccessResult(exists));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.ErrorResult($"Error checking product plan existence: {ex.Message}"));
            }
        }
    }
}
