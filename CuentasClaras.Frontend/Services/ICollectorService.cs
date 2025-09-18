using CuentasClaras.Application.DTOs;
using CuentasClaras.Domain;

namespace CuentasClaras.Frontend.Services
{
    public interface ICollectorService
    {
        Task<IEnumerable<CollectorDto>> GetAllAsync();
        Task<CollectorDto?> GetByIdAsync(int id);
        Task<CollectorDto> CreateAsync(CollectorDto collectorDto);
        Task<bool> UpdateAsync(int id, CollectorDto collectorDto);
        Task<bool> DeleteAsync(int id);
    }
}
