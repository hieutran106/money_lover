using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyLover.Models;

namespace MoneyLover.Controllers
{
    public class IncomeController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        public IncomeController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            return View(repo.GetIncome(user));
        }
        public IActionResult Delete(int id)
        {
            Income income = repo.DeleteIncome(id);
            if (income != null)
            {
                TempData["message"] = "An income was deleted";
            }
            return RedirectToAction("Index");
        }
        public ViewResult Create()
        {
            ViewBag.Categories = new SelectList(repo.Categories, nameof(Category.Name), nameof(Category.Name));
            return View("Edit", new Income() { Date = DateTime.Now });
        }
        public ViewResult Edit(int id)
        {
            Income income = repo.GetIncome(null).Include(e => e.Category).FirstOrDefault(i => i.IncomeId==id);
            ViewBag.Categories = new SelectList(repo.Categories, nameof(Category.Name), nameof(Category.Name), income.Category);
            return View(income);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Income income)
        {
            if (ModelState.IsValid)
            {
                string selectedName = income.Category.Name;
                Category selectedCategory = repo.Categories.FirstOrDefault(c => c.Name == selectedName);
                if (selectedCategory != null)
                {
                    AppUser user = await userManager.GetUserAsync(HttpContext.User);
                    income.User = user;
                    income.Category = selectedCategory;
                    repo.SaveIncome(income);
                    TempData["message"] = "Income has been saved";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = new SelectList(repo.Categories, nameof(Category.Name), nameof(Category.Name));
            return View(income);
        }

    }
}