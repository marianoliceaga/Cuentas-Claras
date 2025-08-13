using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Domain;
using PlanDiario.Infrastructure.Persistence;

namespace PlanDiario.API.Services
{
    public class PlanService : IPlanService
    {
        private readonly PlanDiarioDbContext _context;

        public PlanService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<PlanDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var query = _context.Plans.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();

            var plans = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.ProductPlans)
                    .ThenInclude(pp => pp.Product)
                        .ThenInclude(p => p.Subcategory)
                .ToListAsync();

            var planDtos = plans.Select(MapToPlanDto).ToList();

            return new PagedResult<PlanDto>
            {
                Items = planDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PlanDto?> GetByIdAsync(int id)
        {
            var plan = await _context.Plans
                .Include(p => p.ProductPlans)
                    .ThenInclude(pp => pp.Product)
                        .ThenInclude(p => p.Subcategory)
                .FirstOrDefaultAsync(p => p.Id == id);

            return plan == null ? null : MapToPlanDto(plan);
        }

        public async Task<PlanDto> CreateAsync(CreatePlanDto createPlanDto)
        {
            var plan = new Plan
            {
                Name = createPlanDto.Name,
                StartDate = createPlanDto.StartDate,
                EndDate = createPlanDto.EndDate,
                InterestRate = createPlanDto.InterestRate
            };

            _context.Plans.Add(plan);
            await _context.SaveChangesAsync();

            return MapToPlanDto(plan);
        }

        public async Task<PlanDto?> UpdateAsync(int id, UpdatePlanDto updatePlanDto)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null) return null;

            plan.Name = updatePlanDto.Name;
            plan.StartDate = updatePlanDto.StartDate;
            plan.EndDate = updatePlanDto.EndDate;
            plan.InterestRate = updatePlanDto.InterestRate;

            await _context.SaveChangesAsync();

            return MapToPlanDto(plan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null) return false;

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return true;
        }

        private static PlanDto MapToPlanDto(Plan plan)
        {
            return new PlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                StartDate = plan.StartDate,
                EndDate = plan.EndDate,
                InterestRate = plan.InterestRate,
                ProductPlans = plan.ProductPlans?.Select(pp => new ProductPlanDto
                {
                    Id = pp.Id,
                    PlanId = pp.PlanId,
                    ProductId = pp.ProductId,
                    Product = pp.Product != null ? new ProductDto
                    {
                        Id = pp.Product.Id,
                        Name = pp.Product.Name,
                        Description = pp.Product.Description,
                        Characteristics = pp.Product.Characteristics,
                        CostPrice = pp.Product.CostPrice,
                        SalePrice = pp.Product.SalePrice,
                        Image = pp.Product.Image,
                        SubcategoryId = pp.Product.SubcategoryId,
                        ProviderUrl = pp.Product.ProviderUrl,
                        Subcategory = pp.Product.Subcategory != null ? new SubcategoryDto
                        {
                            Id = pp.Product.Subcategory.Id,
                            Name = pp.Product.Subcategory.Name
                        } : null
                    } : null
                }).ToList() ?? new List<ProductPlanDto>()
            };
        }
    }
}
