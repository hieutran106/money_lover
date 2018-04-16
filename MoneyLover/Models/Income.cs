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


        //define relationships
        public AppUser User { get; set; }
    }
}
