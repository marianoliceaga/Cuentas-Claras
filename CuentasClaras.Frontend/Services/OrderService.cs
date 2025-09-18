using System.Net.Http.Json;
using CuentasClaras.Application.DTOs;

namespace CuentasClaras.Frontend.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>("api/orders");
            return response ?? Enumerable.Empty<OrderDto>();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<OrderDto>($"api/orders/{id}");
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/orders/by-customer/{customerId}");
            return response ?? Enumerable.Empty<OrderDto>();
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDto>() ?? throw new InvalidOperationException();
        }

        public async Task<OrderDto> UpdateOrderAsync(UpdateOrderRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{request.Id}", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<OrderDto>() ?? throw new InvalidOperationException();
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> OrderExistsAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<bool>($"api/orders/{id}/exists");
            return response;
        }
    }
}
