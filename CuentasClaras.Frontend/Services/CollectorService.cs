using CuentasClaras.Application.DTOs;
using CuentasClaras.Domain;
using System.Net.Http.Json;
using static CuentasClaras.Frontend.Services.CollectorService;

namespace CuentasClaras.Frontend.Services
{
        public class CollectorService : ICollectorService
        {
            private readonly HttpClient _httpClient;
            private const string BaseUrl = "api/collectors";

            public CollectorService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<IEnumerable<CollectorDto>> GetAllAsync()
            {
                try
                {
                    var collectors = await _httpClient.GetFromJsonAsync<IEnumerable<CollectorDto>>(BaseUrl);
                    return collectors ?? new List<CollectorDto>();
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error fetching collectors: {ex.Message}", ex);
                }
            }

            public async Task<CollectorDto?> GetByIdAsync(int id)
            {
                try
                {
                    return await _httpClient.GetFromJsonAsync<CollectorDto>($"{BaseUrl}/{id}");
                }
                catch (HttpRequestException ex) when (ex.Data.Contains("StatusCode") && ex.Data["StatusCode"].ToString() == "NotFound")
                {
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error fetching collector: {ex.Message}", ex);
                }
            }

            public async Task<CollectorDto> CreateAsync(CollectorDto collectorDto)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(BaseUrl, collectorDto);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error creating collector: {response.StatusCode} - {errorContent}");
                    }

                    var createdCollector = await response.Content.ReadFromJsonAsync<CollectorDto>();
                    return createdCollector ?? throw new Exception("Failed to deserialize created collector");
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error creating collector: {ex.Message}", ex);
                }
            }

            public async Task<bool> UpdateAsync(int id, CollectorDto collectorDto)
            {
                try
                {
                    collectorDto.Id = id; // Ensure ID consistency
                    var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", collectorDto);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Error updating collector: {response.StatusCode} - {errorContent}");
                    }

                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error updating collector: {ex.Message}", ex);
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
                        throw new Exception($"Error deleting collector: {response.StatusCode} - {errorContent}");
                    }

                    return response.IsSuccessStatusCode;
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception($"Error deleting collector: {ex.Message}", ex);
                }
            }
        }
    }