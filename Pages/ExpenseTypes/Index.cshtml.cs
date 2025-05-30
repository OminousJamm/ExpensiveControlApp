using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpensiveControlApp.Models;
using ExpensiveControlApp.Services.Interfaces;

namespace ExpensiveControlApp.Pages.ExpenseTypes
{
    public class IndexModel : PageModel
    {
        private readonly IExpenseTypeService _expenseTypeService;
        public IndexModel(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        public IList<ExpenseType> ExpenseType { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ExpenseType = (await _expenseTypeService.GetAllAsync()).ToList();
        }
    }
}
