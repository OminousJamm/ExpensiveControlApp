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
    public class IndexModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public IndexModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<MonetaryFund> MonetaryFund { get;set; } = default!;

        public async Task OnGetAsync()
        {
            MonetaryFund = await _context.MonetaryFunds.ToListAsync();
        }
    }
}
