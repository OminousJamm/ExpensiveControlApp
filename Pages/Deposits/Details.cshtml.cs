using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;

namespace ExpensiveControlApp.Pages.Deposits
{
    public class DetailsModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;

        public DetailsModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Deposit Deposit { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits.FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }
            else
            {
                Deposit = deposit;
            }
            return Page();
        }
    }
}
