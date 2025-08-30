namespace PlanDiario.Domain
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public int Quantity { get; set; }
    }
}
