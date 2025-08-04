namespace PlanDiario.Domain
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InterestRate { get; set; }
        
        // Navigation properties
        public ICollection<ProductPlan> ProductPlans { get; set; } = new List<ProductPlan>();
    }
}
