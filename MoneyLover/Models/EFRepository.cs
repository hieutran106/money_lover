using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyLover.Models
{
    public interface IRepository
    {
        IQueryable<Category> Categories { get; }
        IQueryable<Expense> GetExpenses(AppUser user);
    }
    public class EFRepository : IRepository
    {
        private AppDbContext context;
        public EFRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Category> Categories => context.Categories;

        public IQueryable<Expense> GetExpenses(AppUser user)
        {
            var x = context.Expenses;
            return context.Expenses.Where(e => e.User == user);
        }
    }
}
