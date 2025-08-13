using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Application.DTOs
{
    public class ProductPlanDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PlanId { get; set; }

        // Related entities
        public ProductDto? Product { get; set; }
        public PlanDto? Plan { get; set; }

        public int Quantity { get; set; } // Added Quantity
        public decimal ProductPrice { get; set; } // Added ProductPrice
        public string PlanName { get; set; } = string.Empty; // Added PlanName
        public string ProductName { get; set; } = string.Empty; // Added ProductName
        public string CategoryName { get; set; } = string.Empty; // Added CategoryName
        public string SubcategoryName { get; set; } = string.Empty; // Added SubcategoryName
        public string Notes { get; set; } = string.Empty; // Added Notes
    }

    public class CreateProductPlanDto
    {
        public int ProductId { get; set; }
        public int PlanId { get; set; }
    }

    public class UpdateProductPlanDto
    {
        public int ProductId { get; set; }
        public int PlanId { get; set; }
    }
}
