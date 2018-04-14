using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using Microsoft.AspNetCore.Authorization;

namespace MoneyLover.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private IRepository repo;
        public ExpenseController(IRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            AppUser user = HttpContext.User.Identity as AppUser;
            return View(repo.GetExpenses(user));
        }
    }
}