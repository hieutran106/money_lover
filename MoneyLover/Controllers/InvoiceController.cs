using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult Download(int id)
        {
            Invoice dbEntry = repo.Invoices.FirstOrDefault(i => i.InvoiceId == id);
            if (dbEntry!=null)
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(),
                           "invoice", dbEntry.Filename);
                return PhysicalFile(file, "application/pdf");
            } else
            {
                return NotFound();
            }
           
        }
    }
}