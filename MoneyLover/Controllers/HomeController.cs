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
            int fortnightly = 14;
            return await Index(fortnightly);
        }
        [HttpPost] 
        public async Task<IActionResult> Index(int days)
        {
            DateTime now = DateTime.Now;
            DateTime fromDate = now.AddDays(-days);
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Expense> expenses = repo.GetExpenses(user).Where(e => fromDate <= e.Date && e.Date <= now);
            decimal totalExpenses = expenses.Sum(e => e.Amount);
            decimal totalIncomes = repo.GetIncome(user).Where(e => fromDate <= e.Date && e.Date <= now).Sum(e => e.Amount);
            
            OverviewModel model = new OverviewModel
            {
                TotalExpense = totalExpenses,
                TotalIncome = totalIncomes,
                Expense= expenses,
                Username=user.UserName
            };
            return View("Index", model);

        }
    }
}