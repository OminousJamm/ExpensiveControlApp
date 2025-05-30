using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Expenses
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public ExpenseHeader ExpenseHeader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ExpenseHeader = await _context.ExpenseHeaders
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                    .ThenInclude(d => d.ExpenseType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ExpenseHeader == null)
                return NotFound();

            return Page();
        }
    }
}
