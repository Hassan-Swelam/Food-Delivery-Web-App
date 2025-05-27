using Kitchen.Models;

namespace Kitchen.Repository
{
    public interface IOrderRepository:IRepository<Order>
    {
        IEnumerable<Order> GetByCustomer(int customerId);
    }
}
