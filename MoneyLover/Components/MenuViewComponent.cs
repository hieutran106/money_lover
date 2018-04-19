using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Components
{
    public class MenuViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.Controller = RouteData?.Values["controller"];
            string[] data = new string[] { "Home", "Expense", "Income", "Category","Sharing"};
            return View(data);
        }
    }
}
