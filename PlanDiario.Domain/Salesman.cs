namespace PlanDiario.Domain
{
    public class Salesman
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CommissionPercentage { get; set; }
        public string Phone { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
