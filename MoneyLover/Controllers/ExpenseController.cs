using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyLover.Models.ViewModels;

namespace MoneyLover.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        private int pageSize = 2;
        public ExpenseController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            IEnumerable<Expense> expenses = repo.GetExpenses(user);

            return View(new ExpenseListViewModel {
                Expenses = expenses.Skip((page - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = expenses.Count()
                }
            });
        }
        public IActionResult Delete(int id)
        {
            Expense expense = repo.DeleteExpense(id);
            if (expense!=null)
            {
                TempData["message"] = "An expense was deleted";
            }
            return RedirectToAction("Index");
        }
        public ViewResult Create()
        {
            ViewBag.Categories = new SelectList(repo.Categories, nameof(Category.Name), nameof(Category.Name));
            return View("Edit", new Expense() { Date=DateTime.Now, ShareExpense=true});
        }
        public ViewResult Edit(int id)
        {
            Expense expense = repo.GetExpenses(null).Include(e => e.Category).FirstOrDefault(e => e.ExpenseId == id);
            ViewBag.Categories = new SelectList(repo.Categories,nameof(Category.Name),nameof(Category.Name),expense.Category);
            return View(expense);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                string selectedName = expense.Category.Name;
                Category selectedCategory = repo.Categories.FirstOrDefault(c => c.Name == selectedName);
                if (selectedCategory != null)
                {
                    AppUser user = await userManager.GetUserAsync(HttpContext.User);
                    expense.User = user;
                    expense.Category = selectedCategory;
                    repo.SaveExpense(expense);
                    TempData["message"] = "Expense has been saved";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = new SelectList(repo.Categories, nameof(Category.Name), nameof(Category.Name));
            return View(expense);
        }
        
    }
}