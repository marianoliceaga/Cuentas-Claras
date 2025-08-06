using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Characteristics { get; set; } = "{}";
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public byte[]? Image { get; set; }
        public int idSubcategory { get; set; }
        public SubcategoryDto Subcategory { get; set; } = default!;
        public string ProviderUrl { get; set; } = string.Empty;
    }
}
