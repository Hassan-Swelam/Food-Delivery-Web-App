using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(30)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [EmailAddress (ErrorMessage = "Invalid Email Address Fromat\U0001fae0\r\n!")]
        public string Email { get; set; }
        public Customer? Customer { get; set; }
        public Staff? Staff { get; set; }
        public List<AccountRole>? AccountRoles { get; set; }
    }
}
