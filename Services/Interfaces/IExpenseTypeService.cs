using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IExpenseTypeService
    {
        Task<IEnumerable<ExpenseType>> GetAllAsync();
        Task<ExpenseType> GetByIdAsync(int id);
        Task CreateAsync(ExpenseType expenseType);
        Task UpdateAsync(ExpenseType expenseType);
        Task DeleteAsync(int id);
    }
}
