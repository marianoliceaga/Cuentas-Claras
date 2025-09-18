using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<OrderDto> CreateOrderAsync(CreateOrderRequest request);
        Task<OrderDto> UpdateOrderAsync(UpdateOrderRequest request);
        Task<bool> DeleteOrderAsync(Guid id);
        Task<bool> OrderExistsAsync(Guid id);
    }
}
