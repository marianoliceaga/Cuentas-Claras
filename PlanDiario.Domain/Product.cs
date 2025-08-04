namespace PlanDiario.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Characteristics { get; set; } = "{}";
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public byte[]? Image { get; set; }
        public int idSubcategory { get; set; }
        public Subcategory Subcategory { get; set; } = default!;
        public string ProviderUrl { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public ICollection<ProductPlan> ProductPlans { get; set; } = new List<ProductPlan>();
    }
}
