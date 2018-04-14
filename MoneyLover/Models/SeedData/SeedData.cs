using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MoneyLover.Models.SeedData
{
    public static class SeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "Secret123@";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
            

            UserManager<AppUser> userManager = app.ApplicationServices
                                                    .GetRequiredService<UserManager<AppUser>>();

            AppUser user = await userManager.FindByNameAsync(adminUser);
            if (user == null)
            {
                user = new AppUser(adminUser) { Email = "admin@example.com" };
                await userManager.CreateAsync(user, adminPassword);
            }
            if (!context.Categories.Any())
            {
                Category clothingCategory = new Category { Name = "Clothing" };
                Category colesCategory = new Category { Name = "Coles" };
                context.Categories.AddRange(clothingCategory, colesCategory);
                if (!context.Expenses.Any())
                {
                    
                    context.Expenses.AddRange(new Expense {
                        Amount = 12m,
                        Category = clothingCategory,
                        Date = DateTime.Now,
                        Description = "HM",
                        User = user
                    }, new Expense {
                        Amount = 14.85m,
                        Category = colesCategory,
                        Date = DateTime.Now,
                        Description = "Coles",
                        User = user
                    }
                    );

            }
            }
            context.SaveChanges();            
        }
    }
}
