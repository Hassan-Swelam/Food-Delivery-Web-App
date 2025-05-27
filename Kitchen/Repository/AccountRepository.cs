using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class AccountRepository : IAccountRepository
    {
        KitchenContext context;
        public AccountRepository(KitchenContext _Ctx)
        {
            context = _Ctx;
        }
        public List<Account> GetAll()
        {
            return context.Accounts.ToList();
        }
        public Account GetById(int id, string includes = "")
        {
            return context.Accounts.FirstOrDefault(a => a.Id == id);
        }
        public Account GetOne(string username, string password)
        {
            return context.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        }
        public void Add(Account obj)
        {
            context.Accounts.Add(obj);
        }
        public void Edit(Account obj)
        {
            Account accountDB = GetById(obj.Id);
            accountDB.Username = obj.Username;
            accountDB.Password = obj.Password;
            accountDB.Email = obj.Email;
        }
        public void Delete(int id)
        {
            context.Accounts.Remove(GetById(id));
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public List<Account> GetAll(string includes = "")
        {
            throw new NotImplementedException();
        }
    }
}
