using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using MoneyLover.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MoneyLover.Controllers
{
    [Produces("application/json")]
    [Route("api/overview")]
    public class OverviewApiController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        public OverviewApiController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        [HttpGet("{user}/{days}")]
        public async Task<PieChartApiModel> GetAsync(string user,int days)
        {
            DateTime now = DateTime.Now.Date;
            DateTime fromDate = now.AddDays(-days);

            AppUser currUser = await userManager.FindByNameAsync(user);
            IEnumerable<Expense> expenses = repo.GetExpenses(currUser).Where(e => fromDate <= e.Date && e.Date <= now).Include(e => e.Category);
            Dictionary<string, decimal> proportion = new Dictionary<string, decimal>();
            
            foreach (Expense e in expenses)
            {
                if (proportion.ContainsKey(e.Category.Name))
                {
                    proportion[e.Category.Name] += e.Amount;
                } else
                {
                    proportion[e.Category.Name] = e.Amount;
                }
            }
            
            decimal totalExpense = expenses.Sum(e => e.Amount);
            decimal totalIncome = repo.GetIncome(currUser).Where(e => fromDate <= e.Date && e.Date <= now).Sum(e => e.Amount);
            PieChartApiModel apiModel = new PieChartApiModel
            {
                FromDate = fromDate.ToString("dd/MM/yyyy"),
                ToDate = now.ToString("dd/MM/yyyy"),
                TotalIncome = totalIncome.ToString("c"),
                TotalExpense = totalExpense.ToString("c"),
                ExpenseProportion=proportion
            };
            return apiModel;
        }

        [HttpGet("{user}")]
        public async Task<BarChartApiModel> GetAsync(string user)
        {
            AppUser currUser = await userManager.FindByNameAsync(user);
            BarChartApiModel apiModel = new BarChartApiModel();
            //Get data of the last 3 months
            for (int i=0;i<3;i++)
            {
                DateTime time = DateTime.Today.AddMonths(-i);
                string month = time.ToString("MMMM");
                
                decimal totalExpenses = repo.GetExpenses(currUser).Where(e => e.Date.Month == time.Month).Sum(e => e.Amount);
                decimal totalIncome = repo.GetIncome(currUser).Where(e => e.Date.Month == time.Month).Sum(e => e.Amount);

                apiModel.Months[i] = month;
                apiModel.Expenses[i] = totalExpenses;
                apiModel.Incomes[i] = totalIncome;
            }
            
            return apiModel;
        }
    }
}