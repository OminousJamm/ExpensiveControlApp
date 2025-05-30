using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;
using ExpensiveControlApp.Enums;
using ExpensiveControlApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IExpenseService _expenseService;

        public CreateModel(AppDbContext context, IExpenseService expenseService)
        {
            _context = context;
            _expenseService = expenseService;
        }

        [BindProperty]
        public ExpenseHeader ExpenseHeader { get; set; } = new();

        public SelectList MonetaryFunds { get; set; } = default!;
        public SelectList ExpenseTypes { get; set; } = default!;
        public SelectList DocumentTypes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDropdownsAsync();
            ExpenseHeader.Date = DateTime.Today;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || ExpenseHeader.Details.Count == 0)
            {
                await LoadDropdownsAsync();
                if (ExpenseHeader.Details.Count == 0)
                    ModelState.AddModelError(string.Empty, "Debe agregar al menos un gasto al detalle.");
                return Page();
            }

            await _expenseService.CreateAsync(ExpenseHeader);
            return RedirectToPage("/Expenses/Index");
        }

        private async Task LoadDropdownsAsync()
        {
            MonetaryFunds = new SelectList(await _context.MonetaryFunds.ToListAsync(), "Id", "Name");
            ExpenseTypes = new SelectList(await _context.ExpenseTypes.ToListAsync(), "Id", "Name");
            DocumentTypes = new SelectList(Enum.GetValues(typeof(DocumentType)).Cast<DocumentType>());
        }
    }
}
