/*using Microsoft.AspNetCore.Mvc;
using OnlineCateringProject.Models;

namespace OnlineCateringProject.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly OnlineCateringContext _context;

        public InvoiceController(OnlineCateringContext context)
        {
            _context = context;
        }

        public IActionResult CreateInvoice(int orderId)
        {
            var order = _context.CustOrders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var model = new InvoiceViewModel
            {
                OrderId = orderId,
                CostPerPlate = order.CostPerPlate,
                NumberOfPeople = order.MaxPeople,
                TotalCost = order.CostPerPlate * order.MaxPeople,
                DeliveryDate = order.DeliveryDate
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoice(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var invoice = new CustomerInvoice
                {
                    OrderNo = model.OrderId,
                    InvoiceDate = DateTime.Now,
                    TotalAmount = model.TotalCost
                };
                _context.CustomerInvoices.Add(invoice);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewInvoice", new { invoiceId = invoice.InvoiceNo });
            }
            return View(model);
        }

        public async Task<IActionResult> ViewInvoice(int invoiceId)
        {
            var invoice = await _context.CustomerInvoices
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.InvoiceNo == invoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
    }
}
*/