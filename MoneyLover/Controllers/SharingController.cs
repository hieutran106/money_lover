using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using MoneyLover.Models.ViewModels;

using Newtonsoft.Json;
using jsreport.AspNetCore;
using jsreport.Types;
using System.IO;
using System.Text;

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
                SharingViewModel viewModel = await Calculate(model);
                //Save result into TempData
                TempData["invoice"] = JsonConvert.SerializeObject(model);
                return View(viewModel);
            } else
            {
                TempData["message"] = "Please select persons you want to share";
                return RedirectToAction("Index");
            }
            
        }
        
        [MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> Invoice()
        {

            if (TempData["invoice"]!=null)
            {
                string serializedData = TempData["invoice"] as string;
                SharingSelectionModel model = JsonConvert.DeserializeObject<SharingSelectionModel>(serializedData);
                SharingViewModel viewModel = await Calculate(model);
                HttpContext.JsReportFeature().Recipe(Recipe.PhantomPdf).OnAfterRender((report) =>
                {
                    DirectoryInfo directoryInfo= System.IO.Directory.CreateDirectory("invoice");
                    Stream content = report.Content;
                    string path = Path.Combine(directoryInfo.FullName, GenerateFileName());
                    using (FileStream fs = System.IO.File.Create(path))
                    {
                        content.CopyTo(fs);
                    }
                    content.Position = 0;
                    //Write Invoice to DB
                    Invoice dbEntry = new Invoice
                    {
                        CreatedTime = DateTime.Now,
                        Filename = path
                    };
                    repo.SaveInvoice(dbEntry);
                });               
                return View(viewModel);
            } else
            {
                return NotFound();
            }
            
        }
        private async Task<SharingViewModel> Calculate(SharingSelectionModel model)
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
            return viewModel;
        }
        private string GenerateFileName()
        {
            string date = DateTime.Now.ToString("dd_MM_yyyy");
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string filename = "invoice_"  + new String(stringChars)+"_"+date + ".pdf";
            return filename;
        }
    }
}