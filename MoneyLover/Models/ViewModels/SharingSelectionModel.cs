using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MoneyLover.Models.ViewModels
{
    public class SharingSelectionModel
    {
        public string[] Ids { get; set; }
        public string[] Usernames { get; set; }
        public IEnumerable<AppUser> Stakeholders { get; set; }

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
        public void AddUser(AppUser user)
        {
            SharingLine line = new SharingLine { User = user };
            line.CalculateTotalExpense(FromDate, ToDate);
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
        public AppUser User { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Debt { get; set; }
        public void CalculateTotalExpense(DateTime fromDate, DateTime toDate)
        {
            TotalExpense = User.Expenses.Where(e => e.ShareExpense && (fromDate <= e.Date && e.Date <= toDate)).Sum(e => e.Amount);
        }
        public void CalculateDebt(decimal average)
        {
            Debt = TotalExpense - average;
        }
    }
}
