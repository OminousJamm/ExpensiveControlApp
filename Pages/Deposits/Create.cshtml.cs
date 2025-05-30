using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Deposits
{
    public class CreateModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public CreateModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["MonetaryFundId"] = new SelectList(_context.MonetaryFunds, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Deposit Deposit { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Completar las propiedades de navegación
            Deposit.MonetaryFund = await _context.MonetaryFunds.FindAsync(Deposit.MonetaryFundId);

            _context.Deposits.Add(Deposit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
