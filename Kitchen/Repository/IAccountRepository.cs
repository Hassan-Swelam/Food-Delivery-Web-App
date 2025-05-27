using Kitchen.Models;

namespace Kitchen.Repository
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetOne(string username, string password);
    }
}
