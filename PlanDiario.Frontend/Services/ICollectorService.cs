using PlanDiario.Domain;

namespace PlanDiario.Frontend.Services
{
    public interface ICollectorService
    {
        Task<List<Collector>> GetAllCollectorsAsync();
        Task<Collector?> GetCollectorByIdAsync(int id);
    }
}
