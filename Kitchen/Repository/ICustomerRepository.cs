using Kitchen.Models;

namespace Kitchen.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByAccountId(int id);
        Customer GetByPhoneNumber(string number, string Includes = "");
    }
}
