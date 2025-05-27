using Kitchen.Models;

namespace Kitchen.ViewModel
{
    public class ProfileViewModel
    {
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerPhone { get; set; }
        public IEnumerable<Order>? Orders { get; set; }
    }
}
