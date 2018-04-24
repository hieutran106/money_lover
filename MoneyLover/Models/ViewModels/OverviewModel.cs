using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.ViewModels
{
    public class OverviewModel
    {
        public IEnumerable<Expense> Expense { get; set; }
        public string Username { get; set; }
    }
}
