using PlanDiario.Application.DTOs;
using PlanDiario.Domain;
using System.Net.Http.Json;

namespace PlanDiario.Frontend.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "api/salesmen";

        public SalesmanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SalesmanDto>> GetAllAsync()
        {
            try
            {
                var salesmen = await _httpClient.GetFromJsonAsync<IEnumerable<SalesmanDto>>(BaseUrl);
                return salesmen ?? new List<SalesmanDto>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching salesmen: {ex.Message}", ex);
            }
        }

        public async Task<SalesmanDto?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SalesmanDto>($"{BaseUrl}/{id}");
            }
            catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"].ToString() == "NotFound")
            {
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error fetching salesman: {ex.Message}", ex);
            }
        }

        public async Task<SalesmanDto> CreateAsync(SalesmanDto salesmanDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BaseUrl, salesmanDto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error creating salesman: {response.StatusCode} - {errorContent}");
                }

                var createdSalesman = await response.Content.ReadFromJsonAsync<SalesmanDto>();
                return createdSalesman ?? throw new Exception("Failed to deserialize created salesman");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error creating salesman: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(int id, SalesmanDto salesmanDto)
        {
            try
            {
                salesmanDto.Id = id; // Ensure ID consistency
                var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", salesmanDto);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating salesman: {response.StatusCode} - {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error updating salesman: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error deleting salesman: {response.StatusCode} - {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error deleting salesman: {ex.Message}", ex);
            }
        }
    }
}