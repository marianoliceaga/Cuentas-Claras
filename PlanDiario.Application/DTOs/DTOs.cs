namespace PlanDiario.Application.DTOs
{
    public class BusinessTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LineId { get; set; }
        public LineDto Line { get; set; } = default!;
    }

    public class CollectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CommisionPercentage { get; set; }
        public string Phone { get; set; } = string.Empty;
    }

    public class CustomerDto
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
        public LocalityDto Locality { get; set; } = default!;
        public int CollectorId { get; set; }
        public CollectorDto Collector { get; set; } = default!;
        public int SalesmanId { get; set; }
        public SalesmanDto Salesman { get; set; } = default!;
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class LineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class LocalityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public DepartmentDto Department { get; set; } = default!;
    }

    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; } = default!;
        public decimal SellingPrice { get; set; }
        public string PeriodName { get; set; } = string.Empty;
        public int PeriodId { get; set; }
        public PeriodDto Period { get; set; } = default!;
        public int BusinessTypeId { get; set; }
        public BusinessTypeDto BusinessType { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? OrderStartDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public WeekDay WeekDay { get; set; } = WeekDay.Monday;
        public OrderType OrderType { get; set; } = OrderType.Product;
        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
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

    public class OrderProductDto
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; } = default!;
    }

    public class PaymentDto
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal AmountCash { get; set; }
        public decimal AmountTransference { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class PeriodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PlanDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal InterestRate { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Characteristics { get; set; } = "{}";
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public byte[]? Image { get; set; }
        public int idSubcategory { get; set; }
        public SubcategoryDto Subcategory { get; set; } = default!;
        public string ProviderUrl { get; set; } = string.Empty;
    }

    public class ProductPlanDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; } = default!;
        public int PlanId { get; set; }
        public PlanDto Plan { get; set; } = default!;
    }

    public class SalesmanDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CommissionPercentage { get; set; }
        public string Phone { get; set; } = string.Empty;
    }

    public class SubcategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
    }

    // Request/Response DTOs for API
    public class CreateCustomerRequest
    {
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
        public int CollectorId { get; set; }
        public int SalesmanId { get; set; }
    }

    public class UpdateCustomerRequest
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
        public int CollectorId { get; set; }
        public int SalesmanId { get; set; }
    }

    public class CreateOrderRequest
    {
        public string Description { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public decimal SellingPrice { get; set; }
        public int PeriodId { get; set; }
        public int BusinessTypeId { get; set; }
        public DateTime? OrderStartDate { get; set; }
        public WeekDay WeekDay { get; set; } = WeekDay.Monday;
        public OrderType OrderType { get; set; } = OrderType.Product;
        public List<int> ProductIds { get; set; } = new List<int>();
    }

    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public decimal SellingPrice { get; set; }
        public int PeriodId { get; set; }
        public int BusinessTypeId { get; set; }
        public DateTime? OrderStartDate { get; set; }
        public OrderStatus Status { get; set; }
        public WeekDay WeekDay { get; set; }
        public OrderType OrderType { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
    }
}
