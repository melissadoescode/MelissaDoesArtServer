using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public int CheckoutId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
