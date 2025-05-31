using ExpensiveControlApp.Dtos;

namespace ExpensiveControlApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<MovementDto>> GetMovementsAsync(DateTime startDate, DateTime endDate);
        Task<List<BudgetExecutionDto>> GetBudgetExecutionAsync(DateTime startDate, DateTime endDate);
    }
}
