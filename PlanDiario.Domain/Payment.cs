namespace PlanDiario.Domain
{
    public class Payment
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public decimal AmountCash { get; set; }
        public decimal AmountTransference { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
