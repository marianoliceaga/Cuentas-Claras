using System.Net.Http.Json;
using PlanDiario.Application.DTOs;

namespace PlanDiario.Frontend.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDto>>("api/customers");
            return response ?? Enumerable.Empty<CustomerDto>();
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
        }

        public async Task<CustomerDto?> GetCustomerByDNIAsync(string dni)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/by-dni/{dni}");
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerDto>() ?? throw new InvalidOperationException();
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{request.Id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CustomerDto>() ?? throw new InvalidOperationException();
        }

        public async Task<bool> DeleteCustomerAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CustomerExistsAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>($"api/customers/{id}/exists");
            return response;
        }

        public async Task<bool> CustomerExistsByDNIAsync(string dni)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>($"api/customers/dni-exists/{dni}");
            return response;
        }
    }
}
