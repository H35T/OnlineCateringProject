using Microsoft.AspNetCore.Mvc;
using OnlineCateringProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCateringProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccessAdminController : Controller
    {
        readonly OnlineCateringContext db = new();
        [Route("admin/login")]
        [HttpGet]
        public IActionResult LoginAdmin()
        {
            if (HttpContext.Session.GetString("access_admin") != null)
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            return View();
        }
        [Route("admin/login")]
        [HttpPost]
        public IActionResult LoginAdmin(LoginMaster masterAccount)
        {
            if (string.IsNullOrWhiteSpace(masterAccount.Name) || string.IsNullOrWhiteSpace(masterAccount.Password))
            {
                ModelState.AddModelError("", "Username and password cannot be empty.");
                return View(masterAccount);
            }
            var account = db.LoginMasters.Where(x => x.Name == masterAccount.Name && x.Password == HashPassword(masterAccount.Password)).FirstOrDefault();
            if (account == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(masterAccount);
            }
            HttpContext.Session.SetString("access_admin", account.Name);
            HttpContext.Session.SetString("userType", account.UserType);
            
            return RedirectToAction("Index", "HomeAdmin");
        }
        [Route("admin/logout")]
        [HttpGet]
        public IActionResult LogoutAdmin()
        {
            HttpContext.Session.Remove("access_admin");
            HttpContext.Session.Remove("userType");
            return RedirectToAction("LoginAdmin");
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
