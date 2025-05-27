using Kitchen.Models;

namespace Kitchen.ViewModel
{
    public class DishViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; } 

        public List <Category>? Categories { get; set; }
    }
}

