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

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category)
                        .ThenInclude(c => c.Line)
                .ToListAsync();

            return products.Select(MapToDto);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category)
                        .ThenInclude(c => c.Line)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product != null ? MapToDto(product) : null;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsBySubcategoryAsync(int subcategoryId)
        {
            var products = await _context.Products
                .Where(p => p.idSubcategory == subcategoryId)
                .Include(p => p.Subcategory)
                    .ThenInclude(s => s.Category)
                        .ThenInclude(c => c.Line)
                .ToListAsync();

            return products.Select(MapToDto);
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        private static ProductDto MapToDto(Product product)
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
                idSubcategory = product.idSubcategory,
                Subcategory = new SubcategoryDto
                {
                    Id = product.Subcategory.Id,
                    Name = product.Subcategory.Name,
                    CategoryId = product.Subcategory.CategoryId,
                    Category = new CategoryDto
                    {
                        Id = product.Subcategory.Category.Id,
                        Name = product.Subcategory.Category.Name,
                        LineId = product.Subcategory.Category.LineId,
                        Line = new LineDto
                        {
                            Id = product.Subcategory.Category.Line.Id,
                            Name = product.Subcategory.Category.Line.Name
                        }
                    }
                },
                ProviderUrl = product.ProviderUrl
            };
        }
    }
}
