using System.Security.Claims;
using Kitchen.Models;
using Kitchen.Repository;
using Kitchen.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository AccountRepo;
        ICustomerRepository CustomerRepo;
        IRoleRepository RoleRepo;
        public AccountController(IAccountRepository _accountRepo, 
            ICustomerRepository _customerRepository, IRoleRepository _roleRepo)
        {
            AccountRepo = _accountRepo;
            CustomerRepo = _customerRepository;
            RoleRepo = _roleRepo;
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel infoREQ)
        {
            if (ModelState.IsValid)
            {
                if (infoREQ.AccountPassword == infoREQ.Confirm_Password)
                {
                    Account accountDB = new Account()
                    {
                        Username = infoREQ.AccountUserName,
                        Password = infoREQ.AccountPassword,
                        Email = infoREQ.AccountEmail,
                    };
                    AccountRepo.Add(accountDB);
                    AccountRepo.Save();

                    Customer customerDB = new Customer()
                    {
                        Name = infoREQ.AccountUserName,
                        Address = infoREQ.CustomerAddress,
                        PhoneNumber = infoREQ.CustomerPhoneNumber,
                        AccountId = accountDB.Id
                    };
                    CustomerRepo.Add(customerDB);
                    CustomerRepo.Save();

                    AccountRole roleDB = new AccountRole()
                    {
                        AccountID = accountDB.Id,
                        RoleID = 5,
                    };
                    RoleRepo.Add(roleDB);
                    RoleRepo.Save();

                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Password do NOT match");
            }
            return View("Register", infoREQ);
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel infoREQ)
        {
            if (ModelState.IsValid)
            {
                Account accountDB = AccountRepo.GetOne(infoREQ.CustomerUserName, infoREQ.CustomerPassword);
                if (accountDB != null)
                {
                    AccountRole roleDB = RoleRepo.GetById(accountDB.Id);
                    Customer customerDB = CustomerRepo.GetByAccountId(accountDB.Id);

                    ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    if(roleDB.RoleID == 5)
                    {
                        claims.AddClaim(new Claim("CustomerID", customerDB.Id.ToString()));
                    }
                    else
                    {
                        claims.AddClaim(new Claim("AccountID", accountDB.Id.ToString()));
                    }
                    claims.AddClaim(new Claim(ClaimTypes.Name, accountDB.Username));
                    claims.AddClaim(new Claim(ClaimTypes.Role, roleDB.RoleID.ToString()));
                    ClaimsPrincipal principal = new ClaimsPrincipal(claims);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return View("Login", infoREQ);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account" );
        }

        public IActionResult Edit()
        {
            int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
            Customer customerDB = CustomerRepo.GetById(customerID);

            ProfileViewModel profileVM = new ProfileViewModel()
            {
                customerName = customerDB.Name,
                customerPhone = customerDB.PhoneNumber,
                customerAddress = customerDB.Address
            };
            return View("Edit", profileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel profileVM)
        {
            if (ModelState.IsValid)
            {
                int customerID = int.Parse(User.FindFirst("CustomerID")?.Value);
                Customer customerDB = CustomerRepo.GetById(customerID);

                customerDB.Name = profileVM.customerName;
                customerDB.PhoneNumber = profileVM.customerPhone;
                customerDB.Address = profileVM.customerAddress;
                CustomerRepo.Edit(customerDB);
                CustomerRepo.Save();
                return RedirectToAction("Profile", "Customer");
            }
            return View("Edit", profileVM);
        }
    }
}
