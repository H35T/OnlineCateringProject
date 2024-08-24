using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCateringProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly OnlineCateringContext _context;

        public LoginController(OnlineCateringContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
     
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.LoginMasters.Any(u => u.Name == model.Name))
                {
                    ModelState.AddModelError("Name", "Username already exists.");
                    return View(model);
                }
                var user = new LoginMaster
                {
                    Name = model.Name,
                    Password = HashPassword(model.Password),
                    UserType = model.UserType // Customer or Caterer
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                if (model.UserType == "Customer")
                {
                    Customer customer = new()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Phone = model.Phone,
                        Mobile = model.Mobile,
                        PinCode = model.PinCode
                    };
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();


                }
                if (model.UserType == "Caterer")
                {
                    Caterer caterer = new()
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Phone = model.Phone,
                        Mobile = model.Mobile,
                        PinCode = model.PinCode
                    };
                    _context.Caterers.Add(caterer);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
  
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "Username and password cannot be empty.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var user = await _context.LoginMasters.FirstOrDefaultAsync(u => u.Name == model.Name && u.Password == HashPassword(model.Password));
                if (user != null)
                {
                    // Thiết lập thông tin session hoặc cookie
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetString("UserType", user.UserType);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        private static string HashPassword(string input)
        {
            // Chuyển đổi chuỗi thành mảng byte
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // Mã hóa mảng byte và nhận được mảng byte kết quả
            byte[] hashBytes = SHA256.HashData(bytes);

            // Chuyển đổi mảng byte kết quả thành chuỗi hex
            StringBuilder builder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
