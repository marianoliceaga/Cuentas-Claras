namespace CuentasClaras.Domain
{
    public class Collector
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CommisionPercentage { get; set; }
        public string Phone { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
