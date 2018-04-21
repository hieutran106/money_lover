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
        private const string adminPassword = "Secret";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();            

            UserManager<AppUser> userManager = app.ApplicationServices
                                                    .GetRequiredService<UserManager<AppUser>>();

            AppUser user1 = await userManager.FindByNameAsync(adminUser);
            if (user1 == null)
            {
                user1 = new AppUser(adminUser) { Email = "admin@example.com" };
                await userManager.CreateAsync(user1, adminPassword);
            }
            AppUser user2 = await userManager.FindByNameAsync("hieu106");
            if (user2==null)
            {
                user2 = new AppUser("hieu106") { Email = "hieu106@example.com" };
                await userManager.CreateAsync(user2, "Secret");
            }
            if (!context.Categories.Any())
            {
                Category clothingCategory = new Category { Name = "Clothing" };
                Category colesCategory = new Category { Name = "Coles" };
                Category interestCategory = new Category { Name = "Monthly Interest" };
                
                context.Categories.AddRange(clothingCategory, colesCategory);
                if (!context.Expenses.Any())
                {
                    
                    context.Expenses.AddRange(new Expense {
                        Amount = 7m,
                        Category = clothingCategory,
                        Date = DateTime.Today,
                        Description = "HM",
                        ShareExpense=true,
                        User = user1
                    }, new Expense {
                        Amount = 9m,
                        Category = colesCategory,
                        Date = DateTime.Today,
                        Description = "Coles",
                        ShareExpense=true,
                        User = user1
                    }, new Expense {
                        Amount = 5m,
                        Category = colesCategory,
                        Date = DateTime.Today,
                        Description = "Coles",
                        ShareExpense = true,
                        User = user2
                    }, new Expense
                    {
                        Amount = 10m,
                        Category = clothingCategory,
                        Date = DateTime.Today,
                        Description = "Jay Jay",
                        ShareExpense = false,
                        User = user2
                    }
                    );
                }
                if (!context.Incomes.Any())
                {
                    context.Add(new Income
                    {
                        Amount = 220m,
                        Category = interestCategory,
                        Date = DateTime.Today,
                        Description = "Monthly Interest",
                        User = user1
                    });
                }
            }
            context.SaveChanges();            
        }
    }
}
