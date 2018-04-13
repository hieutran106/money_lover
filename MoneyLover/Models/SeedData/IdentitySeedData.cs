using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models.SeedData
{
    public class IdentitySeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Secret123@";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            
            UserManager<AppUser> userManager = app.ApplicationServices
                                                    .GetRequiredService<UserManager<AppUser>>();
            
            AppUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppUser(adminUser) { Email="admin@example.com"};
                await userManager.CreateAsync(user, adminPassword);
            }
        }

    }
}
