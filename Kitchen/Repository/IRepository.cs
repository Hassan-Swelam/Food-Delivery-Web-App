namespace Kitchen.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll(string includes ="");
        T GetById(int id, string includes = "");
        void Add(T obj);
        void Edit(T obj);
        void Delete(int id);
        void Save();
    }
}
