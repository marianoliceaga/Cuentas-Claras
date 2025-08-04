using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsBySubcategoryAsync(int subcategoryId);
        Task<bool> ProductExistsAsync(int id);
    }
}
