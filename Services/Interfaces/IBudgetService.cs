using ExpensiveControlApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> GetAllAsync();
        Task<Budget?> GetByIdAsync(int id);
        Task CreateAsync(Budget budget);
        Task UpdateAsync(Budget budget);
        Task DeleteAsync(int id);
        Task<IEnumerable<Budget>> GetByMonthAndYearAsync(int month, int year);
    }
}
