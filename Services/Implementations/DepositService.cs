using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Services.Implementations
{
    public class DepositService : IDepositService
    {
        private readonly AppDbContext _context;

        public DepositService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Deposit>> GetAllAsync()
        {
            return await _context.Deposits
                .Include(d => d.MonetaryFund)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<Deposit?> GetByIdAsync(int id)
        {
            return await _context.Deposits
                .Include(d => d.MonetaryFund)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task CreateAsync(Deposit deposit)
        {
            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Deposit deposit)
        {
            _context.Deposits.Update(deposit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            if (deposit != null)
            {
                _context.Deposits.Remove(deposit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
