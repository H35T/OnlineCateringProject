using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;
using OnlineCateringProject.Models.Authentication;

namespace OnlineCateringProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatererController : ControllerBase
    {
        private readonly OnlineCateringContext _context;

        public CatererController(OnlineCateringContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string? location)
        {
            List<Caterer> caterers = new();
            if (!string.IsNullOrEmpty(location))
            {
                caterers = await _context.Caterers
               .Where(c => c.Address.Contains(location))
               .ToListAsync();
                return Ok(caterers);
            }
           caterers = await _context.Caterers.ToListAsync();

            return Ok(caterers);
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var name = await _context.Caterers.Where(x => x.CatererId == id).Select(x => x.Name).FirstOrDefaultAsync();
            var menu = await _context.Menus.Where(x => x.CatererId == id).ToListAsync();
            var data = new { name,id, menu };
            return Ok(data);
        }
        [HttpPost("createBill")]
        public async Task<IActionResult> Create([FromBody]BookingRequest request)
        {
            try {
                CustOrder custOrder = new()
                {
                    OrderDate = DateTime.Now,
                    CatererId = request.CatererId,
                    CustomerId = request.CustomerId,
                    CostPerPlate = request.CostPerPlate,
                    DeliveryAddress = request.DeliveryAddress,
                    MaxPeople = request.MaxPeople,
                    MinPeople = request.MinPeople,
                    DeliveryDate = request.DeliveryDate,
                    OrderStatus = "INIT",

                };
                _context.CustOrders.Add(custOrder);
                await _context.SaveChangesAsync();
                int orderNo = _context.CustOrders.Where(x => x.OrderDate == custOrder.OrderDate).Select(x => x.OrderNo).FirstOrDefault();
                List<CustOrderChild> CustOrderChilds = new();
                foreach (var item in request.Orders)
                {
                    CustOrderChild custChild = new()
                    {
                        OrderNo = orderNo,
                        MenuItemNo = item.MenuItemNo,
                        Quantity = item.Quantity,

                    };
                    CustOrderChilds.Add(custChild);
                }
                _context.CustOrderChildren.AddRange(CustOrderChilds);
                await _context.SaveChangesAsync();
                CustomerInvoice customerInvoice = new()
                {
                    CustomerId = request.CustomerId,
                    InvoiceDate = DateTime.Now,
                    OrderNo = orderNo,
                    TotalAmount = (from c in _context.CustOrderChildren
                                   join m in _context.Menus on c.MenuItemNo equals m.MenuItemNo
                                   where c.OrderNo == orderNo
                                   select c.Quantity * m.Price).Sum()
                };
                _context.CustomerInvoices.Add(customerInvoice);
                await _context.SaveChangesAsync();

                return Ok("Thêm thành công");
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet("detailBill/{id}")]
        public async Task<IActionResult> detailBill(int id)
        {
            try {
                var bill = await _context.CustomerInvoices.Where(x=>x.InvoiceNo == id).Select(x => new {x.InvoiceNo,x.InvoiceDate,x.TotalAmount}).FirstOrDefaultAsync();
                var order = (from o in _context.CustOrders
                             join c in _context.CustomerInvoices on o.OrderNo equals c.OrderNo
                             where c.InvoiceNo == bill.InvoiceNo
                             select o).FirstOrDefault();
                var listMenu = (from o in _context.CustOrderChildren
                                join m in _context.Menus on o.MenuItemNo equals m.MenuItemNo
                                where o.OrderNo == order.OrderNo
                                select new
                                {
                                    o.MenuItemNo,
                                    m.ItemName, // Assuming Menu table has a Name field
                                    o.Quantity,
                                    m.Price,
                                }).ToList();
                var data = new
                {
                    bill,
                    order,
                    listMenu
                };
                return Ok(data);

            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

     /*   public IActionResult Book(int catererId)
        {
            var model = new BookingViewModel
            {
                CatererId = catererId,
                // Các thông tin mặc định
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthForAccess]
        public async Task<IActionResult> Book(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new CustOrder
                {
                    CatererId = model.CatererId,
                    CustomerId = _context.Customers.FirstOrDefaultAsync(cus => cus.Name == HttpContext.Session.GetString("access_admin")),
                    OrderDate = DateTime.Now,
                    DeliveryDate = model.DeliveryDate,
                    DeliveryAddress = model.DeliveryAddress,
                    MinPeople = model.MinPeople,
                    MaxPeople = model.MaxPeople,
                    CostPerPlate = model.CostPerPlate
                };
                _context.CustOrders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("BookingSuccess");
            }
            return View(model);
        }*/
    }
}
