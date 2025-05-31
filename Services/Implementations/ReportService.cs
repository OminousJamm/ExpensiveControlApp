using ExpensiveControlApp.Data;
using ExpensiveControlApp.Dtos;
using ExpensiveControlApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovementDto>> GetMovementsAsync(DateTime startDate, DateTime endDate)
        {
            var expenses = await _context.ExpenseHeaders
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .Select(e => new MovementDto
                {
                    Date = e.Date,
                    Type = "Gasto",
                    Amount = e.Details.Sum(d => d.Amount),
                    Description = e.CommerceName
                }).ToListAsync();

            var deposits = await _context.Deposits
                .Where(d => d.Date >= startDate && d.Date <= endDate)
                .Select(d => new MovementDto
                {
                    Date = d.Date,
                    Type = "Depósito",
                    Amount = d.Amount,
                    Description = "Depósito"
                }).ToListAsync();

            return expenses.Concat(deposits).OrderBy(m => m.Date).ToList();
        }

        public async Task<List<BudgetExecutionDto>> GetBudgetExecutionAsync(DateTime startDate, DateTime endDate)
        {
            var budgets = await _context.Budgets
                .Include(b => b.ExpenseType)
                .ToListAsync();

            var expensesGrouped = await _context.ExpenseDetails
                .Where(d => d.ExpenseHeader.Date >= startDate && d.ExpenseHeader.Date <= endDate)
                .GroupBy(d => d.ExpenseTypeId)
                .Select(g => new
                {
                    ExpenseTypeId = g.Key,
                    TotalExecuted = g.Sum(d => d.Amount)
                }).ToListAsync();

            var result = from b in budgets
                         let executed = expensesGrouped.FirstOrDefault(e => e.ExpenseTypeId == b.ExpenseTypeId)?.TotalExecuted ?? 0
                         select new BudgetExecutionDto
                         {
                             ExpenseTypeName = b.ExpenseType.Name,
                             BudgetedAmount = b.Amount,
                             ExecutedAmount = executed
                         };

            return result.ToList();
        }
    }
}
