using ExpensiveControlApp.Dtos;
using ExpensiveControlApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpensiveControlApp.Pages.Reports
{
    public class GetMovementsModel : PageModel
    {

        private readonly IReportService _reportService;

        public GetMovementsModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty]
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-30);

        [BindProperty]
        public DateTime EndDate { get; set; } = DateTime.Today;

        public List<MovementDto> Movements { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (StartDate > EndDate)
            {
                ModelState.AddModelError(string.Empty, "La fecha de inicio no puede ser mayor a la fecha de fin.");
                return Page();
            }

            var result = await _reportService.GetMovementsAsync(StartDate, EndDate);
            Movements = result.ToList();

            return Page();
        }
        public void OnGet()
        {
        }
    }
}
