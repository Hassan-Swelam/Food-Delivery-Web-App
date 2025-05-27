using Kitchen.Models;

namespace Kitchen.ViewModel
{
    public class OrderDishesViewModel
    {
        public string customername { get; set; }
        public string customeraddress { get; set; }
        public string customerphone { get; set; }
        public float orderprice { get; set; }
        public string? orderStatus { get; set; }
        public string? OrderDetailsJson { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
