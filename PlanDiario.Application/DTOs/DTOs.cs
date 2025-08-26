namespace PlanDiario.Application.DTOs
{
    public class BusinessTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class LineDto
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

    public class SubcategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; } = default!;
    }

    public class DepartmentDto
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
    public class PeriodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class OrderProductDto
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderDto Order { get; set; } = default!;
        public int ProductId { get; set; }
        public ProductDto Product { get; set; } = default!;
        public int Quantity { get; set; }
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
