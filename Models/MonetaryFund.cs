using System.ComponentModel.DataAnnotations;

namespace ExpensiveControlApp.Models
{
    public class MonetaryFund
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(MonetaryFundType))]
        public MonetaryFundType Type { get; set; } // e.g., Bank Account, Petty Cash

        public enum MonetaryFundType
        {
            BankAccount,
            PettyCash
        }
    }
}
