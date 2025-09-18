namespace CuentasClaras.Domain
{
    public class ProductPlan
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = default!;
    }
}
