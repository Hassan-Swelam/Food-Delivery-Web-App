using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class DishController : Controller
    {
        IDishRepository DishRepo;
        ICategoryRepository CategoryRepo;
        
        public DishController(IDishRepository _dishRepo , ICategoryRepository _categoryRepo)
        {
            DishRepo = _dishRepo;
            CategoryRepo = _categoryRepo;
            
        }

        public IActionResult All()
        {
            var viewModel = new DishListViewModel
            {
                Dishes = DishRepo.GetAll(),
                Categories = CategoryRepo.GetAll()
            };
            return View("All", viewModel);
            
        }

        public IActionResult Details(int id)
        {
            var dish = DishRepo.GetById(id);
            if (dish == null) return NotFound();

            return View(dish);
        }
        
        public IActionResult Add()
        {
            var model = new DishViewModel();
            model.Categories = CategoryRepo.GetAll();

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DishViewModel DishReq)
        {
            if (DishReq.Name != null)
            {
                string fileName = null;
                if (DishReq.ImageFile != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(DishReq.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        DishReq.ImageFile.CopyTo(stream);
                    }
                }
                Dish newDish = new Dish
                { 
                    Name = DishReq.Name,
                    Price = DishReq.Price,
                    Image = fileName,
                    Description = DishReq.Description,
                    Category_Id = DishReq.CategoryId
                };
                DishRepo.Add(newDish);
                DishRepo.Save();
                return RedirectToAction("All");
            }
            return View("Create", DishReq);
        }

        public IActionResult Edit(int id)
        {
            Dish dish = DishRepo.GetById(id);

            DishViewModel DishVM = new();
            DishVM.Id = dish.Id;
            DishVM.Name = dish.Name;
            DishVM.Image = dish.Image;
            DishVM.Price = dish.Price;
            DishVM.Description = dish.Description;
            DishVM.CategoryId = dish.Category_Id;
            DishVM.Categories= CategoryRepo.GetAll();
            return View("Edit", DishVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DishViewModel DishReq)
        {
            if (ModelState.IsValid)
            {
                Dish? DishDB = DishRepo.GetById(DishReq.Id);

                if (DishDB == null) return NotFound();

                DishDB.Name = DishReq.Name;
                DishDB.Price = DishReq.Price;
                DishDB.Description = DishReq.Description;
                DishDB.Category_Id = DishReq.CategoryId;

                if (DishReq.ImageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(DishReq.ImageFile.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        DishReq.ImageFile.CopyTo(stream);
                    }
                    DishDB.Image = fileName;
                }
                DishRepo.Edit(DishDB);
                DishRepo.Save();

                return RedirectToAction("Details", new { id = DishReq.Id });
            }
            DishReq.Categories = CategoryRepo.GetAll();

            return View("Edit", DishReq);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dish = DishRepo.GetById(id);
            if (dish == null) return NotFound();

            DishRepo.Delete(id);
            DishRepo.Save();

            return RedirectToAction("All");
        }

        public IActionResult Search(string searchString)
        {
            var dishes = DishRepo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return View("All", dishes);
        }
    }
}
