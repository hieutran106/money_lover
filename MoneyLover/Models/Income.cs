using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoneyLover.Models
{
    public class Income
    {
        [BindNever]
        public int IncomeId { get; set; }
        public string Description { get; set; }
        
        //define relationships
        public AppUser User { get; set; }
    }
}
