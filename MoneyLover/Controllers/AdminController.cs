using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using Microsoft.AspNetCore.Identity;

namespace MoneyLover.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public ViewResult Index()
        {
            return View(userManager.Users);
        }
    }
}