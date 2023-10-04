using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccoliteBank.Models
{
    public class AccountType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50)]
        public string Type { get; set; }
        public virtual ICollection<UserAccount> UserAccounts { get; set; }
    }
}