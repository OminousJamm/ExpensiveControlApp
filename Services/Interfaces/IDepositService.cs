using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IDepositService
    {
        Task<IEnumerable<Deposit>> GetAllAsync();
        Task<Deposit?> GetByIdAsync(int id);
        Task CreateAsync(Deposit deposit);
        Task UpdateAsync(Deposit deposit);
        Task DeleteAsync(int id);
    }
}
