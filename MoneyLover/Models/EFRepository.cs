using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoneyLover.Models
{
    public interface IRepository
    {
        IQueryable<Category> Categories { get; }

        IQueryable<Expense> Expenses { get; }
        IQueryable<Expense> GetExpenses(AppUser user);
        Expense DeleteExpense(int expenseId);
        void SaveExpense(Expense expense);

    }
    public class EFRepository : IRepository
    {
        private AppDbContext context;
        public EFRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<Category> Categories => context.Categories;

        public IQueryable<Expense> Expenses => context.Expenses;

        public Expense DeleteExpense(int expenseId)
        {
            Expense dbEntry = context.Expenses.FirstOrDefault(e => e.ExpenseId == expenseId);
            if (dbEntry != null)
            {
                context.Expenses.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IQueryable<Expense> GetExpenses(AppUser user)
        {
            if (user != null)
            {
                return context.Expenses.Where(e => e.User == user).Include(e => e.Category);
            } else
            {
                return context.Expenses.Include(e => e.Category);
            }
            
        }

        public void SaveExpense(Expense expense)
        {
            if (expense.ExpenseId==0)
            {
                context.Add(expense);
            } else
            {
                Expense dbEntry = context.Expenses.FirstOrDefault(e => e.ExpenseId == expense.ExpenseId);
                if (dbEntry!=null)
                {
                    dbEntry.Amount = expense.Amount;
                    dbEntry.Category = expense.Category;
                    dbEntry.Date = expense.Date;
                    dbEntry.Description = expense.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
