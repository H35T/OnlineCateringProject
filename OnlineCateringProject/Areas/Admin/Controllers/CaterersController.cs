using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCateringProject.Models;
using OnlineCateringProject.Models.Authentication;

namespace OnlineCateringProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthForAdmin]
    public class CaterersController : Controller
    {
        private readonly OnlineCateringContext _context;

        public CaterersController(OnlineCateringContext context)
        {
            _context = context;
        }

        // GET: Admin/Caterers
        public async Task<IActionResult> Index()
        {
              return _context.Caterers != null ? 
                          View(await _context.Caterers.ToListAsync()) :
                          Problem("Entity set 'OnlineCateringContext.Caterers'  is null.");
        }

        // GET: Admin/Caterers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Caterers == null)
            {
                return NotFound();
            }

            var caterer = await _context.Caterers
                .FirstOrDefaultAsync(m => m.CatererId == id);
            if (caterer == null)
            {
                return NotFound();
            }

            return View(caterer);
        }

        // GET: Admin/Caterers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Caterers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatererId,Name,Address,PinCode,Phone,Mobile")] Caterer caterer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caterer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(caterer);
        }

        // GET: Admin/Caterers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Caterers == null)
            {
                return NotFound();
            }

            var caterer = await _context.Caterers.FindAsync(id);
            if (caterer == null)
            {
                return NotFound();
            }
            return View(caterer);
        }

        // POST: Admin/Caterers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CatererId,Name,Address,PinCode,Phone,Mobile")] Caterer caterer)
        {
            if (id != caterer.CatererId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caterer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatererExists(caterer.CatererId))
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
            return View(caterer);
        }

        // GET: Admin/Caterers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Caterers == null)
            {
                return NotFound();
            }

            var caterer = await _context.Caterers
                .FirstOrDefaultAsync(m => m.CatererId == id);
            if (caterer == null)
            {
                return NotFound();
            }

            return View(caterer);
        }

        // POST: Admin/Caterers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Caterers == null)
            {
                return Problem("Entity set 'OnlineCateringContext.Caterers'  is null.");
            }
            var caterer = await _context.Caterers.FindAsync(id);
            if (caterer != null)
            {
                _context.Caterers.Remove(caterer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatererExists(int id)
        {
          return (_context.Caterers?.Any(e => e.CatererId == id)).GetValueOrDefault();
        }
    }
}
