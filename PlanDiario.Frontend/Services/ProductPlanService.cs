using PlanDiario.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace PlanDiario.Frontend.Services
{
    public class ProductPlanService : IProductPlanService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductPlanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<PagedResult<ProductPlanDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? planId = null, int? productId = null)
        {
            try
            {
                var query = $"?pageNumber={pageNumber}&pageSize={pageSize}";
                if (planId.HasValue)
                    query += $"&planId={planId}";
                if (productId.HasValue)
                    query += $"&productId={productId}";

                var response = await _httpClient.GetAsync($"api/productplans{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<PagedResult<ProductPlanDto>>>(content, _jsonOptions);
                }
                return ApiResponse<PagedResult<ProductPlanDto>>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResult<ProductPlanDto>>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductPlanDto>?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/productplans/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<ProductPlanDto>>(content, _jsonOptions);
                }
                return ApiResponse<ProductPlanDto>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductPlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<List<ProductPlanDto>>?> GetByPlanIdAsync(int planId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/productplans/plan/{planId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<List<ProductPlanDto>>>(content, _jsonOptions);
                }
                return ApiResponse<List<ProductPlanDto>>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProductPlanDto>>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductPlanDto>?> CreateAsync(CreateProductPlanDto createProductPlanDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/productplans", createProductPlanDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<ProductPlanDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductPlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<ProductPlanDto>?> UpdateAsync(int id, UpdateProductPlanDto updateProductPlanDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/productplans/{id}", updateProductPlanDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<ProductPlanDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProductPlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>?> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/productplans/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>?> CheckExistsAsync(int planId, int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/productplans/exists?planId={planId}&productId={productId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
                }
                return ApiResponse<bool>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Network error: {ex.Message}");
            }
        }
    }
}
