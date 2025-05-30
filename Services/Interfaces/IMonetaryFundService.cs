using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IMonetaryFundService
    {
        Task<IEnumerable<MonetaryFund>> GetAllAsync();
        Task<MonetaryFund> GetByIdAsync(int id);
        Task CreateAsync(MonetaryFund fund);
        Task UpdateAsync(MonetaryFund fund);
        Task DeleteAsync(int id);
    }
}
