using System.ComponentModel.DataAnnotations;

namespace Kitchen.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username must be at most 20 characters.")]
        public string CustomerUserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "Password must be at most 30 characters.")]
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }
    }
}
