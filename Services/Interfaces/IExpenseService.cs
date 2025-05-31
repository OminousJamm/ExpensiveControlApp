using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<List<OverbudgetWarning>> CreateAsync(ExpenseHeader header);
        Task<IEnumerable<ExpenseHeader>> GetAllAsync();
        Task<ExpenseHeader?> GetByIdAsync(int id);
    }
}
