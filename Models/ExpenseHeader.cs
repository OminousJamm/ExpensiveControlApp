using ExpensiveControlApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpensiveControlApp.Models
{
    public class ExpenseHeader
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int MonetaryFundId { get; set; }
        public MonetaryFund? MonetaryFund { get; set; }

        public string? Observations { get; set; }

        [Required]
        [StringLength(100)]
        public string CommerceName { get; set; } = string.Empty;

        [Required]
        public DocumentType DocumentType { get; set; }

        public ICollection<ExpenseDetail> Details { get; set; } = new List<ExpenseDetail>();

    }
}
