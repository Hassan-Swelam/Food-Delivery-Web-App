namespace Kitchen.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        List<Dish> Dishes { get; set; }
    }
}
