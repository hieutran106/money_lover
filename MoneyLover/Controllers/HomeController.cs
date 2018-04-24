using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoneyLover.Models;
using Microsoft.AspNetCore.Identity;
using MoneyLover.Models.ViewModels;

namespace MoneyLover.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        public HomeController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            DateTime now = DateTime.Today;
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Expense> expenses = repo.GetExpenses(user).Take(6);

            OverviewModel model = new OverviewModel
            {
                Expense = expenses,
                Username = user.UserName
            };
            return View("Index", model);
        }
    }
}