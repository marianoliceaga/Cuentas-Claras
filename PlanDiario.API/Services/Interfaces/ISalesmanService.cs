using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Services.Interfaces
{
    public interface ISalesmanService
    {
        Task<IEnumerable<SalesmanDto>> GetAllAsync();
        Task<SalesmanDto?> GetByIdAsync(int id);
        Task<SalesmanDto> CreateAsync(SalesmanDto salesmanDto);
        Task<bool> UpdateAsync(int id, SalesmanDto salesmanDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
