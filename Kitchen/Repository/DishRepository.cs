using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class DishRepository : IDishRepository
    {
        public KitchenContext context;
        public DishRepository(KitchenContext _Ctx)
        {
            context = _Ctx;
        }

        public void Add(Dish obj)
        {
            context.Add(obj);
        }

        public void Delete(int id)
        {
            context.Dishes.Remove(GetById(id));
        }

        public void Edit(Dish obj)
        {
            context.Update(obj);
        }

        public List<Dish> GetAll(string includes = "")
        {
            if (includes == "")
            {
                return context.Dishes.ToList();
            }
            else
            {
                return context.Dishes.Include(includes).ToList();
            }
        }

        public Dish GetById(int id,string includes = "")
        {
            Dish? obj;
            if (string.IsNullOrEmpty(includes))
            {
                obj = context.Dishes.FirstOrDefault(d => d.Id == id);

            }
            else
            {
                obj = context.Dishes.Include(includes).FirstOrDefault(d => d.Id == id);
            }
            return obj;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
