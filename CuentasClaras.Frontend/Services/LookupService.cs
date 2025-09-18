using System.Net.Http.Json;
using CuentasClaras.Application.DTOs;

namespace CuentasClaras.Frontend.Services
{
    public class LookupService : ILookupService
    {
        private readonly HttpClient _httpClient;

        public LookupService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LocalityDto>> GetAllLocalitiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<LocalityDto>>("api/lookups/localities");
            return response ?? Enumerable.Empty<LocalityDto>();
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<DepartmentDto>>("api/lookups/departments");
            return response ?? Enumerable.Empty<DepartmentDto>();
        }

        public async Task<IEnumerable<CollectorDto>> GetAllCollectorsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<CollectorDto>>("api/lookups/collectors");
            return response ?? Enumerable.Empty<CollectorDto>();
        }

        public async Task<IEnumerable<SalesmanDto>> GetAllSalesmenAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<SalesmanDto>>("api/lookups/salesmen");
            return response ?? Enumerable.Empty<SalesmanDto>();
        }

        public async Task<IEnumerable<BusinessTypeDto>> GetAllBusinessTypesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<BusinessTypeDto>>("api/lookups/business-types");
            return response ?? Enumerable.Empty<BusinessTypeDto>();
        }

        public async Task<IEnumerable<PeriodDto>> GetAllPeriodsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<PeriodDto>>("api/lookups/periods");
            return response ?? Enumerable.Empty<PeriodDto>();
        }

        public async Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<SubcategoryDto>>("api/lookups/subcategories");
            return response ?? Enumerable.Empty<SubcategoryDto>();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/lookups/categories");
            return response ?? Enumerable.Empty<CategoryDto>();
        }

        public async Task<IEnumerable<LineDto>> GetAllLinesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<LineDto>>("api/lookups/lines");
            return response ?? Enumerable.Empty<LineDto>();
        }
    }
}
