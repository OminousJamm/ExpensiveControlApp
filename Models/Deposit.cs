using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ExpensiveControlApp.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Monetary Fund")]
        public int MonetaryFundId { get; set; }

        [ForeignKey("MonetaryFundId")]
        [ValidateNever]
        public MonetaryFund MonetaryFund { get; set; } = default!;
    }
}
