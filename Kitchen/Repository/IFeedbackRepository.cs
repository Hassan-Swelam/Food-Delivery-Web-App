using Kitchen.Models;

namespace Kitchen.Repository
{
    public interface IFeedbackRepository:IRepository<Feedback>
    {
        IEnumerable<Feedback> GetByOrder(int orderId);
        IEnumerable<Feedback> GetByCustomer(int customerId);

    }
}
