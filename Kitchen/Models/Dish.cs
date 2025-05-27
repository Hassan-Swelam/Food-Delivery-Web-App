using System.ComponentModel.DataAnnotations.Schema;

namespace Kitchen.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Category")]
        public int Category_Id { get; set; }

        public Category? Category { get; set; }  

        public List<OrderDetails>? OrderDetails{ get; set; }
    }
}
