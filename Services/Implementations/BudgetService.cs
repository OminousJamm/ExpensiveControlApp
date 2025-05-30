using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Services.Interfaces;

namespace ExpensiveControlApp.Services.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAllAsync()
        {
            return await _context.Budgets
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund)
                .ToListAsync();
        }

        public async Task<Budget?> GetByIdAsync(int id)
        {
            return await _context.Budgets
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateAsync(Budget budget)
        {
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Budgets.FindAsync(id);
            if (entity != null)
            {
                _context.Budgets.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Budget>> GetByMonthAndYearAsync(int month, int year)
        {
            return await _context.Budgets
                .Where(b => b.Month == month && b.Year == year)
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund)
                .ToListAsync();
        }
    }
}
