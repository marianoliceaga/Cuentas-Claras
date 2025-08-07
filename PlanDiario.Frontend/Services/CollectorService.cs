using PlanDiario.Domain;
using System.Net.Http.Json;

namespace PlanDiario.Frontend.Services
{
    public class CollectorService : ICollectorService
    {
        private readonly HttpClient _httpClient;

        public CollectorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Collector>> GetAllCollectorsAsync()
        {
            try
            {
                var collectors = await _httpClient.GetFromJsonAsync<List<Collector>>("api/collectors");
                return collectors ?? new List<Collector>();
            }
            catch (Exception)
            {
                return new List<Collector>();
            }
        }

        public async Task<Collector?> GetCollectorByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Collector>($"api/collectors/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}