using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccoliteBank.Models
{
    public class CreateUserAccount
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public int AccountTypeId { get; set; }
    }
}