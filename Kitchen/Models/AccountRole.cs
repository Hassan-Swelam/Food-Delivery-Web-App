using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class AccountRole
    {
        public int Id { get; set; }

        [ForeignKey("Account")]
        public int AccountID { get; set; }
        public Account? Account { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role? Role { get; set; }

    }
}
