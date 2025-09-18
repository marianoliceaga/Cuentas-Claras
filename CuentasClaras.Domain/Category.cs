namespace CuentasClaras.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LineId { get; set; }
        public Line Line { get; set; } = default!;
        
        // Navigation properties
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
