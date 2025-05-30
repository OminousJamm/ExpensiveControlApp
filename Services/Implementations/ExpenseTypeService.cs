using ExpensiveControlApp.Data;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;

namespace ExpensiveControlApp.Services.Implementations
{
    public class ExpenseTypeService : IExpenseTypeService
    {

        private readonly AppDbContext _context;

        public ExpenseTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExpenseType>> GetAllAsync()
            => await _context.ExpenseTypes.ToListAsync();

        public async Task<ExpenseType> GetByIdAsync(int id)
            => await _context.ExpenseTypes.FindAsync(id);

        public async Task CreateAsync(ExpenseType expenseType)
        {
            var last = await _context.ExpenseTypes.OrderByDescending(e => e.Id).FirstOrDefaultAsync();

            var newCode = last == null ? "ET001" : $"ET{(int.Parse(last.Code.Substring(2)) + 1).ToString()}";

            expenseType.Code = newCode;

            _context.ExpenseTypes.Add(expenseType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExpenseType expenseType)
        {
            _context.ExpenseTypes.Update(expenseType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ExpenseTypes.FindAsync(id);
            if (entity != null)
            {
                _context.ExpenseTypes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
