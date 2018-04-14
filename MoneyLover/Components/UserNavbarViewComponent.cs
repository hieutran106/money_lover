using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MoneyLover.Components
{
    public class UserNavbarViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string user = HttpContext.User.Identity.Name;
            return View((object)user);
        }
    }
}
