using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.MonetaryFunds
{
    public class EditModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public EditModel(ExpensiveControlApp.Data.AppDbContext context)
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

            var monetaryfund =  await _context.MonetaryFunds.FirstOrDefaultAsync(m => m.Id == id);
            if (monetaryfund == null)
            {
                return NotFound();
            }
            MonetaryFund = monetaryfund;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MonetaryFund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonetaryFundExists(MonetaryFund.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MonetaryFundExists(int id)
        {
            return _context.MonetaryFunds.Any(e => e.Id == id);
        }
    }
}
