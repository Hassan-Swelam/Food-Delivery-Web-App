using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
      

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("Dish")]
        public int DishId { get; set; }
        public Dish? Dish { get; set; }
    }
}
