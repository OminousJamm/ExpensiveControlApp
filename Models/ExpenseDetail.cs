using System.ComponentModel.DataAnnotations;

namespace ExpensiveControlApp.Models
{
    public class ExpenseDetail
    {
        public int Id { get; set; }

        [Required]
        public int ExpenseHeaderId { get; set; }
        public ExpenseHeader? ExpenseHeader { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }
        public ExpenseType? ExpenseType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
