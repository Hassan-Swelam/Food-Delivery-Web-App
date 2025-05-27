using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Kitchen.Controllers
{
    [Authorize(Roles = "1, 2, 3, 4, 5")]
    public class OrderController : Controller
    {
        IOrderRepository OrderRepo; 
        ICustomerRepository CustomerRepo;
        IOrderDetailsRepository OrderDetailsRepo;

        public OrderController(IOrderDetailsRepository _orderdetailsRepo,
            IOrderRepository _orderRepo, ICustomerRepository _customerRepo)
        {
            OrderRepo = _orderRepo;
            CustomerRepo = _customerRepo;
            OrderDetailsRepo = _orderdetailsRepo;
        }

        [Authorize(Roles = "2 , 4 ")]
        public IActionResult All()
        {
            List<Order> order = OrderRepo.GetAll("Customer");
            return View(order);
        }

        public IActionResult CreateOrder(string orderDetailsJson)
        {
            if (string.IsNullOrEmpty(orderDetailsJson))
            {
                return RedirectToAction("All", "Dish");
            }

            List<OrderDetails> orderDetails = JsonConvert.DeserializeObject<List<OrderDetails>>(orderDetailsJson);

            orderDetails = orderDetails.Where(d => d.Quantity > 0).ToList();

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("All", "Dish");
            }

            float totalPrice = orderDetails.Sum(d => d.Price * d.Quantity);

            var orderdishVM = new OrderDishesViewModel
            {
                OrderDetails = orderDetails,
                orderprice = totalPrice
            };

            return View(orderdishVM);
        }

        public IActionResult OrderDetails(int id)
        {
            var order = OrderRepo.GetById(id);
            if (order == null)
                return NotFound();

            OrderDishesViewModel model = new OrderDishesViewModel()
            {
                customername = order.Customer?.Name,
                customeraddress = order.Customer?.Address,
                customerphone = order.Customer?.PhoneNumber,
                orderprice = order.TotalPrice,
                orderStatus = order.Status,
                OrderDetails = order.OrderDetails
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetails(OrderDishesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateOrder", model);
            }
            int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
            Customer customerDB = CustomerRepo.GetById(customerID);

            customerDB.Name = model.customername;
            customerDB.PhoneNumber = model.customerphone;
            customerDB.Address = model.customeraddress;

            CustomerRepo.Edit(customerDB);
            CustomerRepo.Save();

            var orderDetailsItems = JsonConvert.DeserializeObject<List<OrderDetailsItemViewModel>>(model.OrderDetailsJson);
            var totalPrice = orderDetailsItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                CustomerId = customerDB.Id,
                Date = DateTime.Now,
                TotalPrice = totalPrice,
                Status = "Pending"
            };
            OrderRepo.Add(order);
            OrderRepo.Save();

            HttpContext.Session.SetInt32("OrderID", order.Id);

            foreach (var item in orderDetailsItems)
            {
                var orderDetails = new OrderDetails
                {
                    DishId = item.DishId,
                    OrderId = order.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                OrderDetailsRepo.Add(orderDetails);
            }
            OrderDetailsRepo.Save();
            return RedirectToAction("Profile", "Customer");
        }

        public IActionResult Search(string searchString)
        {
            var orders = OrderRepo.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(d => d.Customer != null && (
                            d.Customer.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                            d.Customer.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                            )).ToList();
            }
            return View("All", orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrderStatus(int orderId)
        {
            var order = OrderRepo.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            order.Status = "Delivered";
            OrderRepo.Edit(order);
            OrderRepo.Save();

            return RedirectToAction("CreateFeedback", "Feedback", new { orderId = orderId });
        }
    }
}