using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using MoneyLover.Models.ViewModels;

using Microsoft.EntityFrameworkCore;
namespace MoneyLover.Controllers
{
    public class SharingController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        public SharingController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            List<AppUser> allUsers = new List<AppUser>();
            foreach (AppUser user in userManager.Users)
            {
                allUsers.Add(user);
            }
            SharingSelectionModel model = new SharingSelectionModel
            {
                Ids= new string[allUsers.Count()],
                Usernames = new string[allUsers.Count()],
                ToDate = DateTime.Today,
                FromDate = DateTime.Today.AddDays(-14)
            };
            for (int i=0;i<allUsers.Count();i++)
            {
                model.Ids[i] = allUsers[i].Id;
                model.Usernames[i] = allUsers[i].UserName;
            }
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Sharing(SharingSelectionModel model)
        {
            if (ModelState.IsValid)
            {
                SharingViewModel viewModel = new SharingViewModel
                {
                    FromDate = model.FromDate,
                    ToDate = model.ToDate
                };

                foreach (string id in model.Ids)
                {
                    AppUser user = await userManager.FindByIdAsync(id);
                    if (user != null)
                    {
                        IEnumerable<Expense> expenses = repo.GetExpenses(user).Where(e => e.ShareExpense && (model.FromDate <= e.Date && e.Date <= model.ToDate));
                        viewModel.AddUser(user.UserName, expenses);
                    }
                }
                viewModel.Calculate();
                return View(viewModel);
            } else
            {
                TempData["message"] = "Please select persons you want to share";
                return RedirectToAction("Index");
            }
            
        }
    }
}