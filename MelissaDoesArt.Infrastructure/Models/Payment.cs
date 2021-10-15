using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelissaDoesArt.Infrastructure.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public string PaymentMessage { get; set; }

    }
}
