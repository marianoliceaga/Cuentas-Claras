using PlanDiario.Domain;

namespace PlanDiario.Frontend.Services
{
    public interface ISalesmanService
    {
        Task<List<Salesman>> GetAllSalesmenAsync();
        Task<Salesman?> GetSalesmanByIdAsync(int id);
    }
}
