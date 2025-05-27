using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Feedback
    {

        public int Id { get; set; }
        public string? Comment { get; set; }
        
        public int Rate { get; set; }

        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public Order? Order { get; set; }
    }
}
