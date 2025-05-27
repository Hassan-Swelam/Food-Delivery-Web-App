using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        KitchenContext context;
        public CategoryRepository(KitchenContext ctx)
        {
            context = ctx;
        }
        public void Add(Category obj)
        {
            context.Categories.Add(obj);
        }

        public void Delete(int id)
        {
            context.Categories.Remove(GetById(id));
        }

        public void Edit(Category obj)
        {
            Category Cat = GetById(obj.Id);

            Cat.Name = obj.Name;
        }

        public List<Category> GetAll(string includes = "")
        {
            if (includes == "")
            {
                return context.Categories.ToList();
            }
            else
            {
                return context.Categories.Include(includes).ToList();
            }
        }

        public Category GetById(int id, string includes = "")
        {
            return context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
