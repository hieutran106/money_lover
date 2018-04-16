using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MoneyLover.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; }
        [Required]
        public String Name { get; set; }
    }
}
