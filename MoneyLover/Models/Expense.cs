using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoneyLover.Models
{
    public class Expense
    {
        [BindNever]
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        //define relationships
        public AppUser User { get; set; }
    }
}
