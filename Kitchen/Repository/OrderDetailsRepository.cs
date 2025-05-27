using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class OrderDetailsRepository: IOrderDetailsRepository
    {
        public KitchenContext context;
        public OrderDetailsRepository(KitchenContext _ctx)
        {
            context = _ctx;
        }

        public void Add(OrderDetails obj)
        {
            context.OrderDetails.Add(obj);
        }

        public void Delete(int id)
        {
            context.OrderDetails.Remove(GetById(id));
        }

        public void Edit(OrderDetails obj)
        {
            context.OrderDetails.Update(obj);
        }

        public List<OrderDetails> GetAll(string includes="")
        {
            List<OrderDetails> list;
            if (string.IsNullOrEmpty(includes))
            {
                list = context.OrderDetails.ToList();

            }
            else
            {
                list = context.OrderDetails.Include(includes).ToList();
            }
            return list;
        }

        public OrderDetails GetById(int id,string includes="")
        {
            OrderDetails obj;
            if (string.IsNullOrEmpty(includes))
            {
                obj = context.OrderDetails.FirstOrDefault(d => d.Id == id);

            }
            else
            {
                obj = context.OrderDetails.Include(includes).FirstOrDefault(d => d.Id == id);
            }
            return obj;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
