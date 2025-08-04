namespace PlanDiario.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Phone3 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int LocalityId { get; set; }
        public Locality Locality { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public int CollectorId { get; set; }
        public Collector Collector { get; set; } = default!;
        public int SalesmanId { get; set; }
        public Salesman Salesman { get; set; } = default!;
    }
}
