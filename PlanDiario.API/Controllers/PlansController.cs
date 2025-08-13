using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        /// <summary>
        /// Get all plans with pagination and optional search
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<PlanDto>>>> GetAllPlans(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            try
            {
                var result = await _planService.GetAllAsync(pageNumber, pageSize, searchTerm);
                return Ok(ApiResponse<PagedResult<PlanDto>>.SuccessResult(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PagedResult<PlanDto>>.ErrorResult($"Error retrieving plans: {ex.Message}"));
            }
        }

        /// <summary>
        /// Get a specific plan by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PlanDto>>> GetPlan(int id)
        {
            try
            {
                var plan = await _planService.GetByIdAsync(id);
                if (plan == null)
                {
                    return NotFound(ApiResponse<PlanDto>.ErrorResult("Plan not found"));
                }

                return Ok(ApiResponse<PlanDto>.SuccessResult(plan));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PlanDto>.ErrorResult($"Error retrieving plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Create a new plan
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PlanDto>>> CreatePlan([FromBody] CreatePlanDto createPlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<PlanDto>.ErrorResult("Validation errors", errors));
                }

                var plan = await _planService.CreateAsync(createPlanDto);
                return CreatedAtAction(nameof(GetPlan), new { id = plan.Id }, ApiResponse<PlanDto>.SuccessResult(plan, "Plan created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PlanDto>.ErrorResult($"Error creating plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Update an existing plan
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<PlanDto>>> UpdatePlan(int id, [FromBody] UpdatePlanDto updatePlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<PlanDto>.ErrorResult("Validation errors", errors));
                }

                var plan = await _planService.UpdateAsync(id, updatePlanDto);
                if (plan == null)
                {
                    return NotFound(ApiResponse<PlanDto>.ErrorResult("Plan not found"));
                }

                return Ok(ApiResponse<PlanDto>.SuccessResult(plan, "Plan updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<PlanDto>.ErrorResult($"Error updating plan: {ex.Message}"));
            }
        }

        /// <summary>
        /// Delete a plan
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeletePlan(int id)
        {
            try
            {
                var result = await _planService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound(ApiResponse<bool>.ErrorResult("Plan not found"));
                }

                return Ok(ApiResponse<bool>.SuccessResult(true, "Plan deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<bool>.ErrorResult($"Error deleting plan: {ex.Message}"));
            }
        }
    }
}
