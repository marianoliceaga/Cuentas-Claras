using Microsoft.EntityFrameworkCore;
using PlanDiario.API.Services.Interfaces;
using PlanDiario.Application.DTOs;
using PlanDiario.Infrastructure.Persistence;
using PlanDiario.Domain;

namespace PlanDiario.API.Services
{
    public class ProductService : IProductService
    {
        private readonly PlanDiarioDbContext _context;

        public ProductService(PlanDiarioDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ProductDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10, string? searchTerm = null, int? subcategoryId = null)
        {
            var query = _context.Products.Include(p => p.Subcategory).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            if (subcategoryId.HasValue)
            {
                query = query.Where(p => p.SubcategoryId == subcategoryId.Value);
            }

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var productDtos = products.Select(MapToProductDto).ToList();

            return new PagedResult<ProductDto>
            {
                Items = productDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? null : MapToProductDto(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Characteristics = createProductDto.Characteristics,
                CostPrice = createProductDto.CostPrice,
                SalePrice = createProductDto.SalePrice,
                Image = createProductDto.Image,
                SubcategoryId = createProductDto.SubcategoryId,
                ProviderUrl = createProductDto.ProviderUrl
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Load subcategory for response
            await _context.Entry(product)
                .Reference(p => p.Subcategory)
                .LoadAsync();

            return MapToProductDto(product);
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Characteristics = updateProductDto.Characteristics;
            product.CostPrice = updateProductDto.CostPrice;
            product.SalePrice = updateProductDto.SalePrice;
            product.Image = updateProductDto.Image;
            product.SubcategoryId = updateProductDto.SubcategoryId;
            product.ProviderUrl = updateProductDto.ProviderUrl;

            await _context.SaveChangesAsync();

            return MapToProductDto(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<SubcategoryDto>> GetSubcategoriesAsync()
        {
            return await _context.Subcategories
                .Select(s => new SubcategoryDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        private static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Characteristics = product.Characteristics,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
                Image = product.Image,
                SubcategoryId = product.SubcategoryId,
                ProviderUrl = product.ProviderUrl,
                Subcategory = product.Subcategory != null ? new SubcategoryDto
                {
                    Id = product.Subcategory.Id,
                    Name = product.Subcategory.Name
                } : null
            };
        }
    }
}
