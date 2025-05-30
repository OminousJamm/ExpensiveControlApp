using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;

namespace ExpensiveControlApp.Pages.Budgets
{
    public class CreateModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;
        private readonly IBudgetService _budgetService;

        public CreateModel(ExpensiveControlApp.Data.AppDbContext context, IBudgetService budgetService)
        {
            _context = context;
            _budgetService = budgetService;
        }

        public IActionResult OnGet()
        {
        ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Id");
        ViewData["MonetaryFundId"] = new SelectList(_context.MonetaryFunds, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Budget Budget { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            if (!ModelState.IsValid)
            {
                ViewData["ExpenseTypeId"] = new SelectList(_context.ExpenseTypes, "Id", "Id");
                ViewData["MonetaryFundId"] = new SelectList(_context.MonetaryFunds, "Id", "Name");
                return Page();
            }

            // Completar las propiedades de navegación
            Budget.ExpenseType = await _context.ExpenseTypes.FindAsync(Budget.ExpenseTypeId);
            Budget.MonetaryFund = await _context.MonetaryFunds.FindAsync(Budget.MonetaryFundId);

            await _budgetService.CreateAsync(Budget);

            return RedirectToPage("./Index");
        }
    }
}
