using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasClaras.Application.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal AmountCash { get; set; }
        public decimal AmountTransference { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
