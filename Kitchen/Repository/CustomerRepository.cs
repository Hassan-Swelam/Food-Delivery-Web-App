using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        KitchenContext context;
        public CustomerRepository(KitchenContext ctx)
        {
            context = ctx;
        }
        public void Add(Customer obj)
        {
            context.Customers.Add(obj);
        }

        public void Delete(int id)
        {
            context.Customers.Remove(GetById(id));  
        }

        public void Edit(Customer obj)
        {
            Customer customer = GetById(obj.Id);

            customer.Address = obj.Address;
            customer.PhoneNumber = obj.PhoneNumber;
            customer.Name = obj.Name;   
            customer.AccountId = obj.AccountId; 
        }

        public List<Customer> GetAll(string inclues="")
        {
            if (inclues == "")
            {
                return context.Customers.ToList();
            }
            else
            {
                return context.Customers.Include(inclues).ToList();
            }
            
        }

        public Customer GetByAccountId(int id)
        {
            return context.Customers.FirstOrDefault(c => c.AccountId == id);
        }

        public Customer GetById(int id, string includes = "")
        {
            return context.Customers.FirstOrDefault(c => c.Id == id);
        }
        public Customer GetByPhoneNumber(string phoneNumber, string Includes = "")
        {
            if (Includes == "")
            {
                return context.Customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            }
            else
            {
                return context.Customers.Include(Includes).FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
