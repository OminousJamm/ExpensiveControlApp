using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.MonetaryFunds
{
    public class DeleteModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public DeleteModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MonetaryFund MonetaryFund { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetaryfund = await _context.MonetaryFunds.FirstOrDefaultAsync(m => m.Id == id);

            if (monetaryfund == null)
            {
                return NotFound();
            }
            else
            {
                MonetaryFund = monetaryfund;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monetaryfund = await _context.MonetaryFunds.FindAsync(id);
            if (monetaryfund != null)
            {
                MonetaryFund = monetaryfund;
                _context.MonetaryFunds.Remove(MonetaryFund);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
