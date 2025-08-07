using PlanDiario.Domain;
using System.Net.Http.Json;

namespace PlanDiario.Frontend.Services
{
    public class SalesmanService : ISalesmanService
    {
        private readonly HttpClient _httpClient;

        public SalesmanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Salesman>> GetAllSalesmenAsync()
        {
            try
            {
                var salesmen = await _httpClient.GetFromJsonAsync<List<Salesman>>("api/salesmen");
                return salesmen ?? new List<Salesman>();
            }
            catch (Exception)
            {
                return new List<Salesman>();
            }
        }

        public async Task<Salesman?> GetSalesmanByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Salesman>($"api/salesmen/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}