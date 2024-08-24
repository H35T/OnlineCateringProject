using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCateringProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly OnlineCateringContext _context;

        public ProfileController(OnlineCateringContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Edit()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userType = HttpContext.Session.GetString("UserType");
            var user = await _context.LoginMasters.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
            {
                return NotFound();
            }
            if (userType == "Customer")
            {
                var infoUser = await _context.Customers.FirstOrDefaultAsync(u => u.Name == userName);
                var model = new ProfileViewModel
                {
                    Address = infoUser?.Address,
                    Phone = infoUser?.Phone,
                    Mobile = infoUser?.Mobile,
                    PinCode = infoUser?.PinCode,
                };
                return View(model);
            }
            if (userType == "Caterer")
            {
                var infoUser = await _context.Caterers.FirstOrDefaultAsync(u => u.Name == userName);
                var model = new ProfileViewModel
                {
                    Address = infoUser?.Address,
                    Phone = infoUser?.Phone,
                    Mobile = infoUser?.Mobile,
                    PinCode = infoUser?.PinCode,
                };
                return View(model);
            }

           
            return View();
        }

        public async Task<IActionResult> EditUser()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userType = HttpContext.Session.GetString("UserType");
            var user = await _context.LoginMasters.FirstOrDefaultAsync(u => u.Name == userName);
            if (user == null)
            {
                return NotFound();
            }
            if (userType == "Customer")
            {
                var infoUser = await _context.Customers.FirstOrDefaultAsync(u => u.Name == userName);
                var model = new ProfileChangeUser
                {
                   Name = userName
                };
                return View(model);
            }
            if (userType == "Caterer")
            {
                var infoUser = await _context.Caterers.FirstOrDefaultAsync(u => u.Name == userName);
                var model = new ProfileChangeUser
                {
                    Name = userName
                };
                return View(model);
            }


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ProfileChangeUser model)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("UserName");
                var userType = HttpContext.Session.GetString("UserType");
                var user = await _context.LoginMasters.FirstOrDefaultAsync(u => u.Name == userName && u.Password == HashPassword(model.OldPassword));
                if (user == null)
                {
                    return NotFound();
                }
                user.Name = model.Name;
                user.Password = HashPassword(model.Password);
                _context.LoginMasters.Update(user);

                if (userType == "Customer")
                {
                    var infoUser = await _context.Customers.FirstOrDefaultAsync(u => u.Name == userName);
                    infoUser.Name = userName;
                    _context.Customers.Update(infoUser);
                }
                if (userType == "Caterer")
                {
                    var infoUser = await _context.Caterers.FirstOrDefaultAsync(u => u.Name == userName);
                    infoUser.Name = userName;
                    _context.Caterers.Update(infoUser);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = HttpContext.Session.GetString("UserName");
                var userType = HttpContext.Session.GetString("UserType");
   

                if (userType == "Customer")
                {
                    var infoUser = await _context.Customers.FirstOrDefaultAsync(u => u.Name == userName);
                    infoUser.Address = model.Address;
                    infoUser.Phone = model.Phone;
                    infoUser.Mobile = model.Mobile;
                    infoUser.PinCode = model.PinCode;
                    infoUser.Name = userName;
                    _context.Customers.Update(infoUser);
                }
                if (userType == "Caterer")
                {
                    var infoUser = await _context.Caterers.FirstOrDefaultAsync(u => u.Name == userName);
                    infoUser.Address = model.Address;
                    infoUser.Phone = model.Phone;
                    infoUser.Mobile = model.Mobile;
                    infoUser.PinCode = model.PinCode;
                    infoUser.Name = userName;
                    _context.Caterers.Update(infoUser);

                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
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
