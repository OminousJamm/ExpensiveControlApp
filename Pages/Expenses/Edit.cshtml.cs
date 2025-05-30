using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;
using ExpensiveControlApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpensiveControlApp.Pages.Expenses
{
    public class EditModel : PageModel
    {
        private readonly IExpenseService _expenseService;
        private readonly AppDbContext _context; // solo para cargar fondos y tipos

        public EditModel(IExpenseService expenseService, AppDbContext context)
        {
            _expenseService = expenseService;
            _context = context;
        }

        [BindProperty]
        public ExpenseHeader ExpenseHeader { get; set; } = default!;

        public SelectList Funds { get; set; } = default!;
        public SelectList Types { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var header = await _expenseService.GetByIdAsync(id.Value);
            if (header == null) return NotFound();

            ExpenseHeader = header;
            await LoadSelectListsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            // Reemplazo de los detalles manualmente en la base de datos
            var existingHeader = await _expenseService.GetByIdAsync(ExpenseHeader.Id);
            if (existingHeader == null)
                return NotFound();

            existingHeader.Date = ExpenseHeader.Date;
            existingHeader.MonetaryFundId = ExpenseHeader.MonetaryFundId;
            existingHeader.CommerceName = ExpenseHeader.CommerceName;
            existingHeader.DocumentType = ExpenseHeader.DocumentType;
            existingHeader.Observations = ExpenseHeader.Observations;

            // Eliminar detalles anteriores y asignar los nuevos
            _context.ExpenseDetails.RemoveRange(existingHeader.Details);
            existingHeader.Details = ExpenseHeader.Details;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private async Task LoadSelectListsAsync()
        {
            Funds = new SelectList(await _context.MonetaryFunds.ToListAsync(), "Id", "Name");
            Types = new SelectList(await _context.ExpenseTypes.ToListAsync(), "Id", "Name");
        }
    }
}
