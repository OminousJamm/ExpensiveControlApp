using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Services.Interfaces;

namespace ExpensiveControlApp.Services.Implementations
{
    public class ExpenseService: IExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ExpenseHeader header)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.ExpenseHeaders.Add(header);
                await _context.SaveChangesAsync();

                if (!header.Details.Any())
                    throw new Exception("Debe ingresar al menos un detalle de gasto.");

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<ExpenseHeader>> GetAllAsync()
        {
            return await _context.ExpenseHeaders
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                    .ThenInclude(d => d.ExpenseType)
                .ToListAsync();
        }

        public async Task<ExpenseHeader?> GetByIdAsync(int id)
        {
            return await _context.ExpenseHeaders
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                    .ThenInclude(d => d.ExpenseType)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
