using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    public class Expense
    {
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public AppUser User { get; set; }
    }
}
