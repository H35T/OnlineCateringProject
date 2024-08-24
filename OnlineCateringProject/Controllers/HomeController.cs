using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;
using OnlineCateringProject.Models.Authentication;
using System.Diagnostics;

namespace OnlineCateringProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineCateringContext _context;
        public HomeController(ILogger<HomeController> logger, OnlineCateringContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var menu = await _context.Menus
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            return View(menu);
        }


        public IActionResult About()
        {
            return View();
        }
        [AuthForAccess]
        public IActionResult Booking(int id)
        {
            var name = _context.Caterers.Where(x => x.CatererId == id).Select(x => x.Name).FirstOrDefault();
            var menu = _context.Menus.Where(x => x.CatererId == id).ToList();
            List<Order> orders = new();
            menu.ForEach(x =>
            {
                Order order = new()
                {
                    MenuItemNo = x.MenuItemNo,
                    Price = x.Price,
                    Quantity = 0,
                    Name = name
                };
                orders.Add(order);
            });

            var model = new BookingRequest
            {
                CatererId = id,

                Orders = orders,

                // Các thông tin mặc định
            };

            return View(model);
        }
        public async Task<IActionResult> Menu()

        {
            var soup = await _context.Menus
                                        .Where(m => m.CategoryId == '1')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var salad = await _context.Menus
                                        .Where(m => m.CategoryId == '2')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var main = await _context.Menus
                                        .Where(m => m.CategoryId == '3')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var desert = await _context.Menus
                                        .Where(m => m.CategoryId == '4')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var lunch = await _context.Menus
                                        .Where(m => m.CategoryId == '5')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var dinner = await _context.Menus
                                        .Where(m => m.CategoryId == '6')
                                        .OrderBy(m => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
                                        .Take(3) // Lấy 3 bản ghi đầu tiên
                                        .ToListAsync();
            var model = new ListMenuClass()
            {
                Soup = soup,
                Salad = salad,
                MainDishes = main,
                Lunch = lunch,
                Dinner = dinner,
                Deserts = desert
            };
            return View(model);
        }
        public async Task<IActionResult> Catering()
        {
            var model = _context.Caterers.OrderBy(m => Guid.NewGuid()).Take(3).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var name = await _context.Caterers.Where(x => x.CatererId == id).Select(x => x.Name).FirstOrDefaultAsync();
            var menu = await _context.Menus.Where(x => x.CatererId == id).ToListAsync();

            var model = new CatererDetailsViewModel
            {
                Name = name,
                id = id,
                Menu = menu
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}