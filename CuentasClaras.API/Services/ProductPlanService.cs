using Microsoft.EntityFrameworkCore;
using CuentasClaras.API.Services.Interfaces;
using CuentasClaras.Application.DTOs;
using CuentasClaras.Domain;
using CuentasClaras.Infrastructure.Persistence;

namespace CuentasClaras.API.Services
{
    public class ProductPlanService : IProductPlanService
    {
        private readonly CuentasClarasDbContext _context;

        public ProductPlanService(CuentasClarasDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductPlanDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, int? planId = null, int? productId = null)
        {
            var query = _context.ProductPlans
                .Include(pp => pp.Plan)
                .Include(pp => pp.Product)
                    .ThenInclude(p => p.Subcategory).ThenInclude(p => p.Category).ThenInclude(c => c.Line)
                .AsQueryable();

            if (planId.HasValue)
            {
                query = query.Where(pp => pp.PlanId == planId.Value);
            }

            if (productId.HasValue)
            {
                query = query.Where(pp => pp.ProductId == productId.Value);
            }

            var totalCount = await query.CountAsync();

            var productPlans = await query
                .OrderBy(pp => pp.Product.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productPlanDtos = productPlans.Select(MapToProductPlanDto).ToList();

            return new PagedResult<ProductPlanDto>
            {
                Items = productPlanDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ProductPlanDto?> GetByIdAsync(int id)
        {
            var productPlan = await _context.ProductPlans
                .Include(pp => pp.Plan)
                .Include(pp => pp.Product)
                    .ThenInclude(p => p.Subcategory)
                .FirstOrDefaultAsync(pp => pp.Id == id);

            return productPlan == null ? null : MapToProductPlanDto(productPlan);
        }

        public async Task<ProductPlanDto> CreateAsync(CreateProductPlanDto createProductPlanDto)
        {
            // Check if the relationship already exists
            var exists = await ExistsAsync(createProductPlanDto.PlanId, createProductPlanDto.ProductId);
            if (exists)
            {
                throw new InvalidOperationException("Product is already associated with this plan");
            }

            var productPlan = new ProductPlan
            {
                PlanId = createProductPlanDto.PlanId,
                ProductId = createProductPlanDto.ProductId
            };

            _context.ProductPlans.Add(productPlan);
            await _context.SaveChangesAsync();

            // Load related entities
            await _context.Entry(productPlan)
                .Reference(pp => pp.Plan)
                .LoadAsync();
            await _context.Entry(productPlan)
                .Reference(pp => pp.Product)
                .LoadAsync();
            await _context.Entry(productPlan.Product)
                .Reference(p => p.Subcategory)
                .LoadAsync();

            return MapToProductPlanDto(productPlan);
        }

        public async Task<ProductPlanDto?> UpdateAsync(int id, UpdateProductPlanDto updateProductPlanDto)
        {
            var productPlan = await _context.ProductPlans
                .Include(pp => pp.Plan)
                .Include(pp => pp.Product)
                    .ThenInclude(p => p.Subcategory)
                .FirstOrDefaultAsync(pp => pp.Id == id);

            if (productPlan == null) return null;

            // Check if the new relationship already exists (if changed)
            if (productPlan.PlanId != updateProductPlanDto.PlanId || productPlan.ProductId != updateProductPlanDto.ProductId)
            {
                var exists = await ExistsAsync(updateProductPlanDto.PlanId, updateProductPlanDto.ProductId);
                if (exists)
                {
                    throw new InvalidOperationException("Product is already associated with this plan");
                }
            }

            productPlan.PlanId = updateProductPlanDto.PlanId;
            productPlan.ProductId = updateProductPlanDto.ProductId;

            await _context.SaveChangesAsync();

            return MapToProductPlanDto(productPlan);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productPlan = await _context.ProductPlans.FindAsync(id);
            if (productPlan == null) return false;

            _context.ProductPlans.Remove(productPlan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProductPlanDto>> GetByPlanIdAsync(int planId)
        {
            var productPlans = await _context.ProductPlans
                .Include(pp => pp.Plan)
                .Include(pp => pp.Product)
                    .ThenInclude(p => p.Subcategory)
                .Where(pp => pp.PlanId == planId)
                .OrderBy(pp => pp.Product.Name)
                .ToListAsync();

            return productPlans.Select(MapToProductPlanDto).ToList();
        }

        public async Task<bool> ExistsAsync(int planId, int productId)
        {
            return await _context.ProductPlans
                .AnyAsync(pp => pp.PlanId == planId && pp.ProductId == productId);
        }

        private static ProductPlanDto MapToProductPlanDto(ProductPlan productPlan)
        {
            return new ProductPlanDto
            {
                Id = productPlan.Id,
                PlanId = productPlan.PlanId,
                ProductId = productPlan.ProductId,
                Plan = productPlan.Plan != null ? new PlanDto
                {
                    Id = productPlan.Plan.Id,
                    Name = productPlan.Plan.Name,
                    StartDate = productPlan.Plan.StartDate,
                    EndDate = productPlan.Plan.EndDate,
                    InterestRate = productPlan.Plan.InterestRate
                } : null,
                Product = productPlan.Product != null ? new ProductDto
                {
                    Id = productPlan.Product.Id,
                    Name = productPlan.Product.Name,
                    Description = productPlan.Product.Description,
                    Characteristics = productPlan.Product.Characteristics,
                    CostPrice = productPlan.Product.CostPrice,
                    SalePrice = productPlan.Product.SalePrice,
                    Image = productPlan.Product.Image,
                    SubcategoryId = productPlan.Product.SubcategoryId,
                    ProviderUrl = productPlan.Product.ProviderUrl,
                    Subcategory = productPlan.Product.Subcategory != null ? new SubcategoryDto
                    {
                        Id = productPlan.Product.Subcategory.Id,
                        Name = productPlan.Product.Subcategory.Name
                    } : null,
                } : null,
                CategoryName = productPlan.Product?.Subcategory?.Name,
                SubcategoryName = productPlan.Product?.Subcategory?.Category?.Name,
                ProductName = productPlan.Product?.Name,
                PlanName = productPlan.Plan?.Name

            };
        }
    }
}
