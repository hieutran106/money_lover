using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;

namespace MoneyLover.Controllers
{
    public class InvoiceController : Controller
    {
        private IRepository repo;
        public InvoiceController(IRepository repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View(repo.Invoices);
        }
        public IActionResult Delete(int id)
        {
            Invoice invoice = repo.DeleteInvoice(id);
            if (invoice != null)
            {
                TempData["message"] = "A category was deleted";
            }
            else
            {
                TempData["message"] = "Could not delete invoice";
            }
            return RedirectToAction("Index");
        }
    }
}