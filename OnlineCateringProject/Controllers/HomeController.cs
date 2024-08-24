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
                    Name = x.ItemName
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
        public IActionResult History()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userType = HttpContext.Session.GetString("UserType");
            int userId;
            if(userType == "Customer")
            {
                userId = _context.Customers.Where(x=>x.Name == userName).Select(id=>id.CustomerId).FirstOrDefault();
                var model = _context.CustomerInvoices.Where(x=>x.CustomerId == userId).ToList();
                return View(model);

            }
            return View();
        }

        public async Task<IActionResult> detailBill(int id)
        {
            try
            {
                // Fetch the invoice details
                var bill = await _context.CustomerInvoices
                    .Where(x => x.InvoiceNo == id)
                    .Select(x => new InvoiceModel
                    {
                        InvoiceNo = x.InvoiceNo,
                        InvoiceDate = x.InvoiceDate,
                        TotalAmount = x.TotalAmount
                    })
                    .FirstOrDefaultAsync();

                if (bill == null)
                {
                    return NotFound("Invoice not found.");
                }

                // Fetch the associated order details
                var order = await (from o in _context.CustOrders
                                   join c in _context.CustomerInvoices on o.OrderNo equals c.OrderNo
                                   where c.InvoiceNo == bill.InvoiceNo
                                   select new OrderModel
                                   {
                                       OrderNo = o.OrderNo,
                                       OrderDate = o.OrderDate,
                                       CatererId = o.CatererId,
                                       CustomerId = o.CustomerId,
                                       DeliveryAddress = o.DeliveryAddress,
                                       MaxPeople = o.MaxPeople,
                                       MinPeople = o.MinPeople,
                                       DeliveryDate = o.DeliveryDate,
                                       OrderStatus = o.OrderStatus
                                   })
                                   .FirstOrDefaultAsync();

                if (order == null)
                {
                    return NotFound("Order not found.");
                }

                // Fetch the associated menu items
                var listMenu = await (from o in _context.CustOrderChildren
                                      join m in _context.Menus on o.MenuItemNo equals m.MenuItemNo
                                      where o.OrderNo == order.OrderNo
                                      select new MenuItemModel
                                      {
                                          MenuItemNo = o.MenuItemNo,
                                          ItemName = m.ItemName,
                                          Quantity = o.Quantity,
                                          Price = m.Price
                                      })
                                      .ToListAsync();

                // Create the InvoiceDetailModel to be passed to the view
                var data = new InvoiceDetailModel
                {
                    Bill = bill,
                    Order = order,
                    ListMenu = listMenu
                };

                return View(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        public async Task<InvoiceDetailModel> GetInvoiceDetails(int id)
        {
            var bill = await _context.CustomerInvoices
                .Where(x => x.InvoiceNo == id)
                .Select(x => new InvoiceModel
                {
                    InvoiceNo = x.InvoiceNo,
                    InvoiceDate = x.InvoiceDate,
                    TotalAmount = x.TotalAmount
                })
                .FirstOrDefaultAsync();

            var order = await (from o in _context.CustOrders
                               join c in _context.CustomerInvoices on o.OrderNo equals c.OrderNo
                               where c.InvoiceNo == bill.InvoiceNo
                               select new OrderModel
                               {
                                   OrderNo = o.OrderNo,
                                   OrderDate = o.OrderDate,
                                   CatererId = o.CatererId,
                                   CustomerId = o.CustomerId,
                                   DeliveryAddress = o.DeliveryAddress,
                                   MaxPeople = o.MaxPeople,
                                   MinPeople = o.MinPeople,
                                   DeliveryDate = o.DeliveryDate,
                                   OrderStatus = o.OrderStatus
                               })
                               .FirstOrDefaultAsync();

            var listMenu = await (from o in _context.CustOrderChildren
                                  join m in _context.Menus on o.MenuItemNo equals m.MenuItemNo
                                  where o.OrderNo == order.OrderNo
                                  select new MenuItemModel
                                  {
                                      MenuItemNo = o.MenuItemNo,
                                      ItemName = m.ItemName, // Assuming Menu table has an ItemName field
                                      Quantity = o.Quantity,
                                      Price = m.Price
                                  })
                                  .ToListAsync();

            var data = new InvoiceDetailModel
            {
                Bill = bill,
                Order = order,
                ListMenu = listMenu
            };

            return data;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId)
        {
            var order = await _context.CustOrders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.OrderStatus = "Success";
            _context.CustOrders.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("OrderHistory");
        }

        public IActionResult OrderHistory()
        {
            var id = _context.Caterers.Where(x=>x.Name == HttpContext.Session.GetString("UserName")).Select(x=>x.CatererId).FirstOrDefault();
            var model = _context.CustOrders.Where(x => x.CatererId == id).ToList();
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}