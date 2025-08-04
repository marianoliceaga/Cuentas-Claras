using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Infrastructure.Persistence;

namespace PlanDiario.API.Services
{
    public class LookupService : ILookupService
    {
        private readonly PlanDiarioDbContext _context;

        public LookupService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocalityDto>> GetAllLocalitiesAsync()
        {
            var localities = await _context.Localities
                .Include(l => l.Department)
                .OrderBy(l => l.Name)
                .ToListAsync();

            return localities.Select(l => new LocalityDto
            {
                Id = l.Id,
                Name = l.Name,
                PostalCode = l.PostalCode,
                DepartmentId = l.DepartmentId,
                Department = new DepartmentDto
                {
                    Id = l.Department.Id,
                    Name = l.Department.Name
                }
            });
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .OrderBy(d => d.Name)
                .ToListAsync();

            return departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            });
        }

        public async Task<IEnumerable<CollectorDto>> GetAllCollectorsAsync()
        {
            var collectors = await _context.Collectors
                .OrderBy(c => c.Name)
                .ToListAsync();

            return collectors.Select(c => new CollectorDto
            {
                Id = c.Id,
                Name = c.Name,
                CommisionPercentage = c.CommisionPercentage,
                Phone = c.Phone
            });
        }

        public async Task<IEnumerable<SalesmanDto>> GetAllSalesmenAsync()
        {
            var salesmen = await _context.Salesmen
                .OrderBy(s => s.Name)
                .ToListAsync();

            return salesmen.Select(s => new SalesmanDto
            {
                Id = s.Id,
                Name = s.Name,
                CommissionPercentage = s.CommissionPercentage,
                Phone = s.Phone
            });
        }

        public async Task<IEnumerable<BusinessTypeDto>> GetAllBusinessTypesAsync()
        {
            var businessTypes = await _context.BusinessTypes
                .OrderBy(bt => bt.Name)
                .ToListAsync();

            return businessTypes.Select(bt => new BusinessTypeDto
            {
                Id = bt.Id,
                Name = bt.Name
            });
        }

        public async Task<IEnumerable<PeriodDto>> GetAllPeriodsAsync()
        {
            var periods = await _context.Periods
                .OrderBy(p => p.Id)
                .ToListAsync();

            return periods.Select(p => new PeriodDto
            {
                Id = p.Id,
                Name = p.Name
            });
        }

        public async Task<IEnumerable<SubcategoryDto>> GetAllSubcategoriesAsync()
        {
            var subcategories = await _context.Subcategories
                .Include(s => s.Category)
                    .ThenInclude(c => c.Line)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return subcategories.Select(s => new SubcategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                CategoryId = s.CategoryId,
                Category = new CategoryDto
                {
                    Id = s.Category.Id,
                    Name = s.Category.Name,
                    LineId = s.Category.LineId,
                    Line = new LineDto
                    {
                        Id = s.Category.Line.Id,
                        Name = s.Category.Line.Name
                    }
                }
            });
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Line)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                LineId = c.LineId,
                Line = new LineDto
                {
                    Id = c.Line.Id,
                    Name = c.Line.Name
                }
            });
        }

        public async Task<IEnumerable<LineDto>> GetAllLinesAsync()
        {
            var lines = await _context.Lines
                .OrderBy(l => l.Name)
                .ToListAsync();

            return lines.Select(l => new LineDto
            {
                Id = l.Id,
                Name = l.Name
            });
        }
    }
}
