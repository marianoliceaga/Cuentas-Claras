namespace PlanDiario.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<Locality> Localities { get; set; } = new List<Locality>();
    }
}
