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

        public async Task<List<OverbudgetWarning>> CreateAsync(ExpenseHeader header)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            var warnings = new List<OverbudgetWarning>();

            try
            {
                if (!header.Details.Any())
                    throw new Exception("Debe ingresar al menos un detalle de gasto.");

                var currentMonth = header.Date.Month;
                var currentYear = header.Date.Year;

                foreach (var detail in header.Details)
                {
                    var budget = await _context.Budgets
                        .FirstOrDefaultAsync(b =>
                            b.ExpenseTypeId == detail.ExpenseTypeId &&
                            b.Month == currentMonth &&
                            b.Year == currentYear);

                    var prevTotal = await _context.ExpenseDetails
                        .Where(d =>
                            d.ExpenseTypeId == detail.ExpenseTypeId &&
                            d.ExpenseHeader.Date.Month == currentMonth &&
                            d.ExpenseHeader.Date.Year == currentYear)
                        .SumAsync(d => d.Amount);

                    var totalAfter = prevTotal + detail.Amount;

                    if (budget != null && totalAfter > budget.Amount)
                    {
                        var typeName = await _context.ExpenseTypes
                            .Where(e => e.Id == detail.ExpenseTypeId)
                            .Select(e => e.Name)
                            .FirstAsync();

                        warnings.Add(new OverbudgetWarning
                        {
                            ExpenseTypeName = typeName,
                            BudgetedAmount = budget.Amount,
                            ExecutedAmount = totalAfter
                        });
                    }
                }

                _context.ExpenseHeaders.Add(header);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return warnings;
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
