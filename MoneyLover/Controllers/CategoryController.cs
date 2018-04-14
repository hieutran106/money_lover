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
    }
}