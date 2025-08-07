using PlanDiario.Domain;
using System.Net.Http.Json;

namespace PlanDiario.Frontend.Services
{
    public class LocalityService : ILocalityService
    {
        private readonly HttpClient _httpClient;

        public LocalityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Locality>> GetAllLocalitiesAsync()
        {
            try
            {
                var localities = await _httpClient.GetFromJsonAsync<List<Locality>>("api/localities");
                return localities ?? new List<Locality>();
            }
            catch (Exception)
            {
                return new List<Locality>();
            }
        }

        public async Task<Locality?> GetLocalityByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Locality>($"api/localities/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}