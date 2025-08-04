using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsBySubcategoryAsync(int subcategoryId);
        Task<bool> ProductExistsAsync(int id);
    }
}
