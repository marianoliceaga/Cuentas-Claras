using CuentasClaras.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace CuentasClaras.Frontend.Services
{
    public class PlanService : IPlanService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public PlanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ApiResponse<PagedResult<PlanDto>>?> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            try
            {
                var query = $"?pageNumber={pageNumber}&pageSize={pageSize}";
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    query += $"&searchTerm={Uri.EscapeDataString(searchTerm)}";

                var response = await _httpClient.GetAsync($"api/plans{query}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<PagedResult<PlanDto>>>(content, _jsonOptions);
                }
                return ApiResponse<PagedResult<PlanDto>>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<PagedResult<PlanDto>>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<PlanDto>?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/plans/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ApiResponse<PlanDto>>(content, _jsonOptions);
                }
                return ApiResponse<PlanDto>.ErrorResult($"Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<PlanDto>?> CreateAsync(CreatePlanDto createPlanDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/plans", createPlanDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<PlanDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<PlanDto>?> UpdateAsync(int id, UpdatePlanDto updatePlanDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/plans/{id}", updatePlanDto);
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<PlanDto>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<PlanDto>.ErrorResult($"Network error: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>?> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/plans/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<bool>>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ErrorResult($"Network error: {ex.Message}");
            }
        }
    }
}
