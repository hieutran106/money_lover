using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Filename { get; set; }
    }
}
