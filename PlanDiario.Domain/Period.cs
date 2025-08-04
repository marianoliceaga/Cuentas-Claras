namespace PlanDiario.Domain
{
    public class Period
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
