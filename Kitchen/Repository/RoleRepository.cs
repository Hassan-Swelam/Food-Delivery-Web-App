using Kitchen.Models;

namespace Kitchen.Repository
{
    public class RoleRepository : IRoleRepository
    {
        KitchenContext context;
        public RoleRepository(KitchenContext _Ctx)
        {
            context = _Ctx;
        }
        public List<AccountRole> GetAll(string includes = "")
        {
            return context.AccountRoles.ToList();
        }
        public AccountRole GetById(int id, string includes = "")
        {
            return context.AccountRoles.FirstOrDefault(a => a.AccountID == id);
        }
        public void Add(AccountRole obj)
        {
            context.AccountRoles.Add(obj);
        }
        public void Edit(AccountRole obj)
        {
            AccountRole roleDB = GetById(obj.Id);
            roleDB.AccountID = obj.AccountID;
            roleDB.RoleID = obj.RoleID;
        }
        public void Delete(int id)
        {
            context.AccountRoles.Remove(GetById(id));
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}