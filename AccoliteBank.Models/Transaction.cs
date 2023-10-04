using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccoliteBank.Models
{
    public class Transaction
    {
        [Required(ErrorMessage = "Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}