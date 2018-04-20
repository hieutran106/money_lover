using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace MoneyLover.Models
{
    public class Expense
    {
        
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public bool ShareExpense { get; set; }
        //define relationships
        public AppUser User { get; set; }
    }
}
