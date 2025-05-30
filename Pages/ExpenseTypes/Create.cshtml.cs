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

namespace ExpensiveControlApp.Pages.ExpenseTypes
{
    public class CreateModel : PageModel
    {
        IExpenseTypeService _expenseTypeService;

        public CreateModel(ExpensiveControlApp.Data.AppDbContext context, IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ExpenseType ExpenseType { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _expenseTypeService.CreateAsync(ExpenseType);

            return RedirectToPage("./Index");
        }
    }
}
