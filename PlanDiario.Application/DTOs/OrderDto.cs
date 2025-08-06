using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Application.DTOs
{
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
}
