using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MoneyLover.Models
{
    public class Category
    {
        [BindNever]
        public int CategoryId { get; set; }
        public String Name { get; set; }
    }
}
