using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, int? subcategoryId = null);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto);
        Task<bool> DeleteAsync(int id);
        Task<List<SubcategoryDto>> GetSubcategoriesAsync();
    }
}
