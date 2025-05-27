using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    //[Authorize(Roles = "4")]
    public class CustomerController : Controller
    {
        ICustomerRepository CustomerRepo;
        IOrderRepository OrderRepo;
        IOrderDetailsRepository OrderDetailsRepo;
        public CustomerController(ICustomerRepository _customerRepo, IOrderRepository _orderRepo, IOrderDetailsRepository _orderdetailsRepo)
        {
            CustomerRepo = _customerRepo;
            OrderRepo = _orderRepo;
            OrderDetailsRepo = _orderdetailsRepo;
        }
        public IActionResult Profile()
        {
            int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
            Customer customerDB = CustomerRepo.GetById(customerID);
            IEnumerable<Order> orderDB = OrderRepo.GetByCustomer(customerID);

            if (customerDB != null)
            {
                ProfileViewModel ProfileVM = new ProfileViewModel()
                {
                    customerName = customerDB.Name,
                    customerAddress = customerDB.Address,
                    customerPhone = customerDB.PhoneNumber,
                    Orders = orderDB,
                };
                return View("Profile", ProfileVM);
            }
            return NotFound();
        }
    }
}