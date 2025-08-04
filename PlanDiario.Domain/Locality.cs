namespace PlanDiario.Domain
{
    public class Locality
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;
        
        // Navigation properties
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}
