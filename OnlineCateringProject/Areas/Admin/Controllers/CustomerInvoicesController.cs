using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;

namespace OnlineCateringProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerInvoicesController : Controller
    {
        private readonly OnlineCateringContext _context;

        public CustomerInvoicesController(OnlineCateringContext context)
        {
            _context = context;
        }

        // GET: Admin/CustomerInvoices
        public async Task<IActionResult> Index()
        {
            var onlineCateringContext = _context.CustomerInvoices.Include(c => c.Customer).Include(c => c.OrderNoNavigation);
            return View(await onlineCateringContext.ToListAsync());
        }

        // GET: Admin/CustomerInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices
                .Include(c => c.Customer)
                .Include(c => c.OrderNoNavigation)
                .FirstOrDefaultAsync(m => m.InvoiceNo == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // GET: Admin/CustomerInvoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            ViewData["OrderNo"] = new SelectList(_context.CustOrders, "OrderNo", "OrderNo");
            return View();
        }

        // POST: Admin/CustomerInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceNo,InvoiceDate,OrderNo,CustomerId,TotalAmount")] CustomerInvoice customerInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", customerInvoice.CustomerId);
            ViewData["OrderNo"] = new SelectList(_context.CustOrders, "OrderNo", "OrderNo", customerInvoice.OrderNo);
            return View(customerInvoice);
        }

        // GET: Admin/CustomerInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", customerInvoice.CustomerId);
            ViewData["OrderNo"] = new SelectList(_context.CustOrders, "OrderNo", "OrderNo", customerInvoice.OrderNo);
            return View(customerInvoice);
        }

        // POST: Admin/CustomerInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceNo,InvoiceDate,OrderNo,CustomerId,TotalAmount")] CustomerInvoice customerInvoice)
        {
            if (id != customerInvoice.InvoiceNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInvoiceExists(customerInvoice.InvoiceNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", customerInvoice.CustomerId);
            ViewData["OrderNo"] = new SelectList(_context.CustOrders, "OrderNo", "OrderNo", customerInvoice.OrderNo);
            return View(customerInvoice);
        }

        // GET: Admin/CustomerInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerInvoices == null)
            {
                return NotFound();
            }

            var customerInvoice = await _context.CustomerInvoices
                .Include(c => c.Customer)
                .Include(c => c.OrderNoNavigation)
                .FirstOrDefaultAsync(m => m.InvoiceNo == id);
            if (customerInvoice == null)
            {
                return NotFound();
            }

            return View(customerInvoice);
        }

        // POST: Admin/CustomerInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerInvoices == null)
            {
                return Problem("Entity set 'OnlineCateringContext.CustomerInvoices'  is null.");
            }
            var customerInvoice = await _context.CustomerInvoices.FindAsync(id);
            if (customerInvoice != null)
            {
                _context.CustomerInvoices.Remove(customerInvoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerInvoiceExists(int id)
        {
          return (_context.CustomerInvoices?.Any(e => e.InvoiceNo == id)).GetValueOrDefault();
        }
    }
}
