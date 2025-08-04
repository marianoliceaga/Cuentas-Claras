namespace PlanDiario.Domain
{
    public class Line
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Navigation properties
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
