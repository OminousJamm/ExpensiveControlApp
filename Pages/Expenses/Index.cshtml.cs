using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<ExpenseHeader> ExpenseHeaders { get; set; } = new List<ExpenseHeader>();

        public async Task OnGetAsync()
        {
            ExpenseHeaders = await _context.ExpenseHeaders
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                    .ThenInclude(d => d.ExpenseType)
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }
    }
}
