namespace ExpensiveControlApp.Models
{
    public class OverbudgetWarning
    {
        public string ExpenseTypeName { get; set; } = string.Empty;
        public decimal BudgetedAmount { get; set; }
        public decimal ExecutedAmount { get; set; }
        public decimal OverAmount => ExecutedAmount - BudgetedAmount;
    }
}
