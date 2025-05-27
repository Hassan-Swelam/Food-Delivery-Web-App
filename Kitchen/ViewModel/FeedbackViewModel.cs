using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kitchen.ViewModel
{
    public class FeedbackViewModel
    {
        [Display(Name = "Tell us a little more")]
        [MaxLength(100)]
        public string Comment { get; set; }
        public int OrderRate { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}
