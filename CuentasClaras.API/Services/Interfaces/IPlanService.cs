using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Services.Interfaces
{
    public interface IPlanService
    {
        Task<PagedResult<PlanDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<PlanDto?> GetByIdAsync(int id);
        Task<PlanDto> CreateAsync(CreatePlanDto createPlanDto);
        Task<PlanDto?> UpdateAsync(int id, UpdatePlanDto updatePlanDto);
        Task<bool> DeleteAsync(int id);
    }
}
