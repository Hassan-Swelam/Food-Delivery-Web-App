using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "5")]
    public class FeedbackController : Controller
    {
        IFeedbackRepository FeedbackRepo; 
        public FeedbackController(IFeedbackRepository _feedbackRepo)
        {
            FeedbackRepo = _feedbackRepo;
        }

        [Authorize(Roles = "1, 2, 3, 4")]
        public IActionResult All()
        {
            List<Feedback> feedBackList = FeedbackRepo.GetAll();
            return View("All", feedBackList);
        }
        public IActionResult Random()
        {
            var allFeedbacks = FeedbackRepo.GetAll("Customer");
            var random = new Random();
            var randomFeedbacks = allFeedbacks.OrderBy(x => random.Next()).Take(5).ToList();
            return View(randomFeedbacks);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFeedback(int orderId)
        {
            var feedbackorderVM = new FeedbackViewModel
            {
                OrderId = orderId,
            };
            return View(feedbackorderVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFeedback(FeedbackViewModel feedbackVM)
        {
            if (ModelState.IsValid)
            {
                var feedback = new Feedback
                {
                    Comment = feedbackVM.Comment,
                    Rate = feedbackVM.OrderRate,
                    OrderId = feedbackVM.OrderId,
                    Customer_ID = int.Parse(User.FindFirst("CustomerID")?.Value)
                };
                FeedbackRepo.Add(feedback);
                FeedbackRepo.Save();

                return RedirectToAction("FeedbackSuccess"); 
            }
            return View(feedbackVM); 
        }

        public IActionResult FeedbackSuccess()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
