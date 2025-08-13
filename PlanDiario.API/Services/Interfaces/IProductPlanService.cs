using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Services.Interfaces
{
    public interface IProductPlanService
    {
        Task<PagedResult<ProductPlanDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? planId = null, int? productId = null);
        Task<ProductPlanDto?> GetByIdAsync(int id);
        Task<ProductPlanDto> CreateAsync(CreateProductPlanDto createProductPlanDto);
        Task<ProductPlanDto?> UpdateAsync(int id, UpdateProductPlanDto updateProductPlanDto);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductPlanDto>> GetByPlanIdAsync(int planId);
        Task<bool> ExistsAsync(int planId, int productId);
    }
}
