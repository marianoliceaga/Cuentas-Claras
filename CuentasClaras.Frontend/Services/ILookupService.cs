using CuentasClaras.Application.DTOs;

namespace CuentasClaras.Frontend.Services
{
    public interface ILookupService
    {
        Task<IEnumerable<LocalityDto>> GetAllLocalitiesAsync();
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<CollectorDto>> GetAllCollectorsAsync();
        Task<IEnumerable<SalesmanDto>> GetAllSalesmenAsync();
        Task<IEnumerable<BusinessTypeDto>> GetAllBusinessTypesAsync();
        Task<IEnumerable<PeriodDto>> GetAllPeriodsAsync();
        Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync();
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<LineDto>> GetAllLinesAsync();
    }
}
