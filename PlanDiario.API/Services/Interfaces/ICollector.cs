using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Services.Interfaces
{
    public interface ICollectorService
    {
        Task<IEnumerable<CollectorDto>> GetAllAsync();
        Task<CollectorDto?> GetByIdAsync(int id);
        Task<CollectorDto> CreateAsync(CollectorDto collectorDto);
        Task<bool> UpdateAsync(int id, CollectorDto collectorDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
