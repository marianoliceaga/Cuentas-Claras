using PlanDiario;

namespace PlanDiario.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
        public decimal SellingPrice { get; set; }
        public string PeriodName { get; set; } = string.Empty;
        public int PeriodId { get; set; }
        public Period Period { get; set; } = default!;
        public int BusinessTypeId { get; set; }
        public BusinessType BusinessType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? OrderStartDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public WeekDay WeekDay { get; set; } = WeekDay.Monday;
        public OrderType OrderType { get; set; } = OrderType.Product;
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }

    public enum OrderStatus
    {
        Created,
        InProgress,
        Completed,
        Cancelled
    }

    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum OrderType
    {
        Product,
        Cash
    }
}
