using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace MoneyLover.Models
{
    public class Income
    {
        
        public int IncomeId { get; set; }
        public string Description { get; set; }
        [UIHint("Date")]
        public DateTime Date;
        [Required]
        public Category Category { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Amount { get; set; }
        //define relationships
        public AppUser User { get; set; }
    }
}
