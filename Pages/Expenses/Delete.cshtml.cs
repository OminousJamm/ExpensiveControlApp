using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ExpenseHeader ExpenseHeader { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            ExpenseHeader = await _context.ExpenseHeaders
                .Include(e => e.MonetaryFund)
                .Include(e => e.Details)
                .ThenInclude(d => d.ExpenseType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ExpenseHeader == null) return NotFound();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var header = await _context.ExpenseHeaders
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (header != null)
            {
                _context.ExpenseDetails.RemoveRange(header.Details);
                _context.ExpenseHeaders.Remove(header);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
