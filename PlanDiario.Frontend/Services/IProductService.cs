using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public interface IProductService
    {
        Task<ApiResponse<PagedResult<ProductDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, int? subcategoryId = null);
        Task<ApiResponse<ProductDto>?> GetByIdAsync(int id);
        Task<ApiResponse<ProductDto>?> CreateAsync(CreateProductDto createProductDto);
        Task<ApiResponse<ProductDto>?> UpdateAsync(int id, UpdateProductDto updateProductDto);
        Task<ApiResponse<bool>?> DeleteAsync(int id);
        Task<ApiResponse<List<SubcategoryDto>>?> GetSubcategoriesAsync();
    }
}
