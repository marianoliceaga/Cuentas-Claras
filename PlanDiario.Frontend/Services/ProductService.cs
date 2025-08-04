using System.Net.Http.Json;
using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/products");
            return response ?? Enumerable.Empty<ProductDto>();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySubcategoryAsync(int subcategoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"api/products/by-subcategory/{subcategoryId}");
            return response ?? Enumerable.Empty<ProductDto>();
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>($"api/products/{id}/exists");
            return response;
        }
    }
}
