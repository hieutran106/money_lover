using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoneyLover.Models;
using MoneyLover.Models.ViewModels;

namespace MoneyLover.Controllers
{
    [Produces("application/json")]
    [Route("api/overview")]
    public class OverviewApiController : Controller
    {
        private IRepository repo;
        private UserManager<AppUser> userManager;
        public OverviewApiController(IRepository repo, UserManager<AppUser> userManager)
        {
            this.repo = repo;
            this.userManager = userManager;
        }
        [HttpGet("{user}/{days}")]
        public ApiModel Get(string user,int days)
        {
            ApiModel apiModel = new ApiModel
            {
                FromDate = "aaaa",
                ToDate = "bbbb",
                TotalIncome = 12.43m.ToString("c"),
                TotalExpense = 50m.ToString("c")
            };
            return apiModel;
        }
    }
}