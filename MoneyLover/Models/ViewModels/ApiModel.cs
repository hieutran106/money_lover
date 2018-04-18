using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.ViewModels
{
    public class PieChartApiModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalIncome { get; set; }
        public string TotalExpense { get; set; }
        public Dictionary<string, decimal> ExpenseProportion { get; set; }
    }
    public class BarChartApiModel
    {
        public string[] Months = new string[3];
        public decimal[] Incomes = new decimal[3];
        public decimal[] Expenses = new decimal[3];
        
    }
}
