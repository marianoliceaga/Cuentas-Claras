using PlanDiario.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace PlanDiario.Frontend.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<PagedResult<ProductDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, int? subcategoryId = null)
        {
            try
            {
                var query = $"?pageNumber={pageNumber}&pageSize={pageSize}";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    query += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";
                if (subcategoryId.HasValue)
                    query += $"&subcategoryId={subcategoryId}";

                var response = await _httpClient.GetAsync($"api/products{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<PagedResult<ProductDto>>>(content, _jsonOptions);
                }
                return ApiResponse<PagedResult<ProductDto>>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResult<ProductDto>>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductDto>?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/products/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<ProductDto>>(content, _jsonOptions);
                }
                return ApiResponse<ProductDto>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductDto>?> CreateAsync(CreateProductDto createProductDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/products", createProductDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<ProductDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductDto>?> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", updateProductDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<ProductDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>?> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/products/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<SubcategoryDto>>?> GetSubcategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/products/subcategories");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<List<SubcategoryDto>>>(content, _jsonOptions);
                }
                return ApiResponse<List<SubcategoryDto>>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<SubcategoryDto>>.ErrorResult($"Network error: {ex.Message}");
            }
        }
    }
}
