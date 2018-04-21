using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoneyLover.Models.ViewModels
{
    public class SharingSelectionModel
    {
        [Required]
        public string[] Ids { get; set; }
        public string[] Usernames { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

    }
    
    public class SharingViewModel
    {
        public List<SharingLine> Lines{ get; set; }
        public decimal Total { get; set; }
        public decimal Average { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public SharingViewModel()
        {
            Lines = new List<SharingLine>();
        }
        public void AddUser(string userName, IEnumerable<Expense> expenses)
        {
            SharingLine line = new SharingLine {
                UserName =userName,
                Expenses = expenses
            };
            line.CalculateTotalExpense();
            Lines.Add(line);
        }
        public void Calculate()
        {
            Total = Lines.Sum(l => l.TotalExpense);
            Average = Total / Lines.Count();
            foreach (SharingLine line in Lines)
            {
                line.CalculateDebt(Average);
            }
        }
    }
    public class SharingLine
    {
        public string UserName { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Debt { get; set; }
        public void CalculateTotalExpense()
        {
            TotalExpense = Expenses.Sum(e => e.Amount);
        }
        public void CalculateDebt(decimal average)
        {
            Debt = TotalExpense - average;
        }
    }
}
