using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"^(\+20|0)(10|11|12|15)\d{8}$", ErrorMessage = "Please enter a valid phone number 😡\r\n ")]
        public string PhoneNumber { get; set; }

        public List<Order>? Orders { get; set; }
        public List<Feedback>? Feedbacks { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }

    }
}
