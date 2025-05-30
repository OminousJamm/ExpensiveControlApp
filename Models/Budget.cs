using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExpensiveControlApp.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }

        [ForeignKey(nameof(ExpenseTypeId))]
        [ValidateNever]
        public ExpenseType ExpenseType { get; set; } = default!;

        [Required]
        public int MonetaryFundId { get; set; }

        [ForeignKey(nameof(MonetaryFundId))]
        [ValidateNever]
        public MonetaryFund MonetaryFund { get; set; } = default!;

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }  // mes específico (1-12)

        [Required]
        [Range(2000, 2100)]
        public int Year { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ingresar un valor positivo.")]
        public decimal Amount { get; set; }
    }
}
