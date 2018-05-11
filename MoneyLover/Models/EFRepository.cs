using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoneyLover.Models
{
    public interface IRepository
    {
        //category
        IQueryable<Category> Categories { get; }
        Category DeleteCategory(int categoryId);
        void SaveCategory(Category category);

        IQueryable<Expense> Expenses { get; }
        IQueryable<Expense> GetExpenses(AppUser user);
        Expense DeleteExpense(int expenseId);
        void SaveExpense(Expense expense);
        //income
        IQueryable<Income> Incomes { get; }
        IQueryable<Income> GetIncome(AppUser user);
        Income DeleteIncome(int incomeId);
        void SaveIncome(Income income);

        //Invoice
        IQueryable<Invoice> Invoices { get; }
        Invoice DeleteInvoice(int invoiceId);
        void SaveInvoice(Invoice invoice);
        

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

        public IQueryable<Income> Incomes => context.Incomes;

        public IQueryable<Invoice> Invoices => context.Invoices;

        public Category DeleteCategory(int categoryId)
        {
            bool hasInexpenseRelation=context.Expenses.Include(e => e.Category).Any(e => e.Category.CategoryId == categoryId);
            bool hasIncomeRelation = context.Incomes.Include(e => e.Category).Any(e => e.Category.CategoryId == categoryId);
            if (hasInexpenseRelation|| hasIncomeRelation)
            {
                return null;
            } else
            {
                Category dbEntry = context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
                if (dbEntry != null)
                {
                    context.Categories.Remove(dbEntry);
                    context.SaveChanges();
                }
                return dbEntry;
            }         
        }

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

        public Income DeleteIncome(int incomeId)
        {
            Income dbEntry = context.Incomes.FirstOrDefault(i => i.IncomeId==incomeId);
            if (dbEntry != null)
            {
                context.Incomes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Invoice DeleteInvoice(int invoiceId)
        {
            Invoice dbEntry = context.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            if (dbEntry!=null)
            {
                //delete physical file
                string file = Path.Combine(Directory.GetCurrentDirectory(),
                           "invoice", dbEntry.Filename);
                File.Delete(file);
                context.Invoices.Remove(dbEntry);
                context.SaveChanges();               
            }
            return dbEntry;
        }

        public IQueryable<Expense> GetExpenses(AppUser user)
        {
            if (user != null)
            {
                return context.Expenses.Where(e => e.User == user).Include(e => e.Category).OrderByDescending(e => e.Date);
            } else
            {
                return context.Expenses.Include(e => e.Category);
            }
            
        }

        public IQueryable<Income> GetIncome(AppUser user)
        {
            if (user != null)
            {
                return context.Incomes.Where(e => e.User == user).Include(i => i.Category).OrderByDescending(e => e.Date);
            }
            else
            {
                return context.Incomes.Include(e => e.Category);
            }
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryId==0)
            {
                context.Add(category);
            } else
            {
                Category dbEntry = context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                }
            }
            context.SaveChanges();
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
                    dbEntry.ShareExpense = expense.ShareExpense;
                }
            }
            context.SaveChanges();
        }

        public void SaveIncome(Income income)
        {
            if (income.IncomeId == 0)
            {
                context.Add(income);
            }
            else
            {
                Income dbEntry = context.Incomes.FirstOrDefault(i => i.IncomeId == income.IncomeId);
                if (dbEntry != null)
                {
                    dbEntry.Amount = income.Amount;
                    dbEntry.Category = income.Category;
                    dbEntry.Date = income.Date;
                    dbEntry.Description = income.Description;
                }
            }
            context.SaveChanges();
        }

        public void SaveInvoice(Invoice invoice)
        {
            if (invoice.InvoiceId == 0)
            {
                context.Add(invoice);
            }
            context.SaveChanges();
        }
    }
}
