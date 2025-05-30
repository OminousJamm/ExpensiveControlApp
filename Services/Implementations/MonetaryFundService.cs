using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Services.Implementations
{
    public class MonetaryFundService : IMonetaryFundService
    {

        private readonly AppDbContext _context;

        public MonetaryFundService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MonetaryFund>> GetAllAsync() =>
           await _context.MonetaryFunds.ToListAsync();

        public async Task<MonetaryFund> GetByIdAsync(int id) =>
            await _context.MonetaryFunds.FindAsync(id);

        public async Task CreateAsync(MonetaryFund fund)
        {
            _context.MonetaryFunds.Add(fund);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MonetaryFund fund)
        {
            _context.MonetaryFunds.Update(fund);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fund = await _context.MonetaryFunds.FindAsync(id);
            if (fund != null)
            {
                _context.MonetaryFunds.Remove(fund);
                await _context.SaveChangesAsync();
            }
        }

    }
}
