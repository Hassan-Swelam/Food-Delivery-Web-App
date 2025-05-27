using System.ComponentModel.DataAnnotations;

namespace Kitchen.ViewModel
{
    public class RegistrationViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username must be at most 20 characters.")]
        public string AccountUserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password must be at most 30 characters.")]
        [DataType(DataType.Password)]
        public string AccountPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("AccountPassword", ErrorMessage = "Passwords do not match.")]
        public string Confirm_Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format!")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string CustomerAddress { get; set; }

        [Required]
        [RegularExpression(@"^(\+20|0)(10|11|12|15)\d{8}$", ErrorMessage = "Please enter a valid phone number.")]
        public string CustomerPhoneNumber { get; set; }
    }
}
