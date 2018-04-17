using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.ViewModels
{
    public class ApiModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalIncome { get; set; }
        public string TotalExpense { get; set; }
        public Dictionary<string, decimal> ExpenseProportion { get; set; }
    }
}
