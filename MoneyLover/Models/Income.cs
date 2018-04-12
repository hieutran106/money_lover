using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    public class Income
    {
        public string Description { get; set; }
        public AppUser User { get; set; }
    }
}
