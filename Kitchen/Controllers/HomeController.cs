using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kitchen.Controllers
{
    public class HomeController : Controller
    {
        IStaffRepository StaffRepository;
        IFeedbackRepository FeedbackRepository;
        ICategoryRepository CategoryRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IFeedbackRepository feedbackRepository, ICategoryRepository categoryRepository, IStaffRepository staffRepository)
        {
            _logger = logger;
            FeedbackRepository = feedbackRepository;
            CategoryRepository = categoryRepository;
            StaffRepository = staffRepository;
        }

        public IActionResult Index()
        {
            var MainVM = new MainViewModel
            {
                Feedbacks = FeedbackRepository.GetAll("Customer"),    
                Staffs = StaffRepository.GetAll(),
                Categories = CategoryRepository.GetAll(),
            };

            return View(MainVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
