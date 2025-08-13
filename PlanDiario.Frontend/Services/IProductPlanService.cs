using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public interface IProductPlanService
    {
        Task<ApiResponse<PagedResult<ProductPlanDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? planId = null, int? productId = null);
        Task<ApiResponse<ProductPlanDto>?> GetByIdAsync(int id);
        Task<ApiResponse<List<ProductPlanDto>>?> GetByPlanIdAsync(int planId);
        Task<ApiResponse<ProductPlanDto>?> CreateAsync(CreateProductPlanDto createProductPlanDto);
        Task<ApiResponse<ProductPlanDto>?> UpdateAsync(int id, UpdateProductPlanDto updateProductPlanDto);
        Task<ApiResponse<bool>?> DeleteAsync(int id);
        Task<ApiResponse<bool>?> CheckExistsAsync(int planId, int productId);
    }
}
