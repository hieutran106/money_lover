using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoneyLover.Models
{
    public class AppUser: IdentityUser
    {
        public AppUser(): base()
        {

        }
        public AppUser(string userName): base(userName)
        {

        }
        ICollection<Expense> Expenses { get; set; }
        ICollection<Income> Incomes { get; set; }
    }
}
