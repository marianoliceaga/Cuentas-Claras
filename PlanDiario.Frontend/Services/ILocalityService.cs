using PlanDiario.Domain;

namespace PlanDiario.Frontend.Services
{
    public interface ILocalityService
    {
        Task<List<Locality>> GetAllLocalitiesAsync();
        Task<Locality?> GetLocalityByIdAsync(int id);
    }
}
