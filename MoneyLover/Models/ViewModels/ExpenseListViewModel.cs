using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.ViewModels
{
    public class ExpenseListViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
