using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Application.DTOs
{
    public class UpdateOrderRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public int PeriodId { get; set; }
        public int BusinessTypeId { get; set; }
        public DateTime? OrderStartDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public WeekDay WeekDay { get; set; }
        public OrderType OrderType { get; set; }
        public List<int> ProductIds { get; set; } = new List<int>();
        public int Quantity { get; set; } = 1;
    }
}
