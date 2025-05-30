using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Budgets
{
    public class IndexModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public IndexModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Budget> Budget { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Budget = await _context.Budgets
                .Include(b => b.ExpenseType)
                .Include(b => b.MonetaryFund).ToListAsync();
        }
    }
}
