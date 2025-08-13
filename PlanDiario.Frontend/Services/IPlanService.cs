using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public interface IPlanService
    {
        Task<ApiResponse<PagedResult<PlanDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<ApiResponse<PlanDto>?> GetByIdAsync(int id);
        Task<ApiResponse<PlanDto>?> CreateAsync(CreatePlanDto createPlanDto);
        Task<ApiResponse<PlanDto>?> UpdateAsync(int id, UpdatePlanDto updatePlanDto);
        Task<ApiResponse<bool>?> DeleteAsync(int id);
    }
}
