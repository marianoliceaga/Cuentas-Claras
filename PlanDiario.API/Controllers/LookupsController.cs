using Microsoft.AspNetCore.Mvc;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;

namespace PlanDiario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LookupsController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupsController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("localities")]
        public async Task<ActionResult<IEnumerable<LocalityDto>>> GetLocalities()
        {
            var localities = await _lookupService.GetAllLocalitiesAsync();
            return Ok(localities);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _lookupService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("collectors")]
        public async Task<ActionResult<IEnumerable<CollectorDto>>> GetCollectors()
        {
            var collectors = await _lookupService.GetAllCollectorsAsync();
            return Ok(collectors);
        }

        [HttpGet("salesmen")]
        public async Task<ActionResult<IEnumerable<SalesmanDto>>> GetSalesmen()
        {
            var salesmen = await _lookupService.GetAllSalesmenAsync();
            return Ok(salesmen);
        }

        [HttpGet("business-types")]
        public async Task<ActionResult<IEnumerable<BusinessTypeDto>>> GetBusinessTypes()
        {
            var businessTypes = await _lookupService.GetAllBusinessTypesAsync();
            return Ok(businessTypes);
        }

        [HttpGet("periods")]
        public async Task<ActionResult<IEnumerable<PeriodDto>>> GetPeriods()
        {
            var periods = await _lookupService.GetAllPeriodsAsync();
            return Ok(periods);
        }

        [HttpGet("subcategories")]
        public async Task<ActionResult<IEnumerable<SubcategoryDto>>> GetSubcategories()
        {
            var subcategories = await _lookupService.GetAllSubcategoriesAsync();
            return Ok(subcategories);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _lookupService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("lines")]
        public async Task<ActionResult<IEnumerable<LineDto>>> GetLines()
        {
            var lines = await _lookupService.GetAllLinesAsync();
            return Ok(lines);
        }
    }
}
