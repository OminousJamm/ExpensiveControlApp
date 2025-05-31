using ExpensiveControlApp.Services.Interfaces;
using ExpensiveControlApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExpensiveControlApp.Pages.Reports
{
    public class BudgetExecutionChartModel : PageModel
    {
        private readonly IReportService _reportService;

        public BudgetExecutionChartModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime From { get; set; } = DateTime.Today.AddMonths(-1);

        [BindProperty(SupportsGet = true)]
        public DateTime To { get; set; } = DateTime.Today;

        public List<BudgetExecutionDto> ChartData { get; set; } = new();

        public async Task OnGetAsync()
        {
            ChartData = await _reportService.GetBudgetExecutionAsync(From, To);
        }
    }
}
