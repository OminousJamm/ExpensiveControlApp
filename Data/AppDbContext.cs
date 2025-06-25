using ExpensiveControlApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<ExpenseType> ExpenseTypes { get; set; } = null!;
        public DbSet<MonetaryFund> MonetaryFunds { get; set; } = null!;
        public DbSet<Budget> Budgets { get; set; } = default!;
        public DbSet<ExpenseHeader> ExpenseHeaders { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }

}
