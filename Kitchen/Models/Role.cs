using System.ComponentModel.DataAnnotations;

namespace Kitchen.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }
        public List<AccountRole>? AccountRoles { get; set; }
    }
}
