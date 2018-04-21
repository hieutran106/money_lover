using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;

namespace MoneyLover.Controllers
{
    public class CategoryController : Controller
    {
        private IRepository repo;
        public CategoryController(IRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View(repo.Categories);
        }
        public IActionResult Delete(int id)
        {
            Category category = repo.DeleteCategory(id);
            if (category != null)
            {
                TempData["message"] = "A category was deleted";
            } else
            {
                TempData["message"] = "Could not delete category, because it is used in a relation with an expense or income";
            }
            return RedirectToAction("Index");
        }
        public ViewResult Create()
        {
            return View("Edit", new Category());
        }
        public ViewResult Edit(int id)
        {
            Category category = repo.Categories.FirstOrDefault(c => c.CategoryId == id);            
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repo.SaveCategory(category);
                TempData["message"] = "A new category has been saved";
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}