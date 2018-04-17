using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.ViewModels
{
    public class OverviewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public IEnumerable<Expense> Expense { get; set; }
        public string Username { get; set; }
    }
}
