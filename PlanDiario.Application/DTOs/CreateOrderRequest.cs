using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanDiario.Application.DTOs
{
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
}
