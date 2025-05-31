namespace ExpensiveControlApp.Dtos
{
    public class BudgetExecutionDto
    {
        public string ExpenseTypeName { get; set; } = string.Empty;
        public decimal BudgetedAmount { get; set; }
        public decimal ExecutedAmount { get; set; }
    }
}
