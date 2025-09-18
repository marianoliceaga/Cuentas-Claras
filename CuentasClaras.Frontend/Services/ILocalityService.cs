using CuentasClaras.Domain;

namespace CuentasClaras.Frontend.Services
{
    public interface ILocalityService
    {
        Task<List<Locality>> GetAllLocalitiesAsync();
        Task<Locality?> GetLocalityByIdAsync(int id);
    }
}
