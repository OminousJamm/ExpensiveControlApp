using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpensiveControlApp.Data;
using ExpensiveControlApp.Models;
using static ExpensiveControlApp.Models.MonetaryFund;

namespace ExpensiveControlApp.Pages.MonetaryFunds
{
    public class CreateModel : PageModel
    {
        private readonly ExpensiveControlApp.Data.AppDbContext _context;
        public List<SelectListItem> TypeOptions { get; set; } = new();


        public CreateModel(ExpensiveControlApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            TypeOptions = Enum.GetValues(typeof(MonetaryFundType))
                .Cast<MonetaryFundType>()
                .Select(t => new SelectListItem
                {
                    Value = ((int)t).ToString(),
                    Text = t.ToString()
                })
                .ToList();

            return Page();
        }


        [BindProperty]
        public MonetaryFund MonetaryFund { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TypeOptions = Enum.GetValues(typeof(MonetaryFundType))
                        .Cast<MonetaryFundType>()
                        .Select(t => new SelectListItem
                        {
                            Value = ((int)t).ToString(),
                            Text = t.ToString()
                        })
                        .ToList();
                return Page();
            }

            _context.MonetaryFunds.Add(MonetaryFund);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
