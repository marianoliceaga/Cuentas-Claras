using CuentasClaras.Application.DTOs;

namespace CuentasClaras.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
        Task<CustomerDto?> GetCustomerByDNIAsync(string dni);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerRequest request);
        Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerRequest request);
        Task<bool> DeleteCustomerAsync(Guid id);
        Task<bool> CustomerExistsAsync(Guid id);
        Task<bool> CustomerExistsByDNIAsync(string dni);
    }
}
