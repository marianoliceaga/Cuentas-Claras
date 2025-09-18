using System.Net.Http.Json;
using CuentasClaras.Application.DTOs;

namespace CuentasClaras.Frontend.Services
{
    public interface ISalesmanService
    {
        Task<IEnumerable<SalesmanDto>> GetAllAsync();
        Task<SalesmanDto?> GetByIdAsync(int id);
        Task<SalesmanDto> CreateAsync(SalesmanDto salesmanDto);
        Task<bool> UpdateAsync(int id, SalesmanDto salesmanDto);
        Task<bool> DeleteAsync(int id);
    }
}
