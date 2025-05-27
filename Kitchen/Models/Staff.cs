using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Staff
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Name must be less than 25 character")]
        [MinLength(3, ErrorMessage = "Name must be more than 2 character")]
        public string Name { get; set; }
        public string Position { get; set; }
        [Range(10000, 20000, ErrorMessage = "Salary must be between 10000 and 20000")]
        public float Salary { get; set; }

        public string? Image {  get; set; } 
       
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
