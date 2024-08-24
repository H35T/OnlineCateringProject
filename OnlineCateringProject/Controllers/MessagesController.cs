/*using Microsoft.AspNetCore.Mvc;

namespace OnlineCateringProject.Controllers
{
    public class MessagesController : Controller
    {
        private readonly OnlineCateringContext _context;

        public MessagesController(OnlineCateringContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Inbox()
        {
            var userId = GetUserIdFromSession();
            var messages = await _context.Messages
                .Where(m => m.ToUserId == userId)
                .ToListAsync();

            return View(messages);
        }

        public IActionResult SendMessage(int toUserId)
        {
            var model = new MessageViewModel
            {
                ToUserId = toUserId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    FromUserId = GetUserIdFromSession(),
                    ToUserId = model.ToUserId,
                    MessageText = model.MessageText,
                    SentDate = DateTime.Now
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return RedirectToAction("Inbox");
            }
            return View(model);
        }
    }

public class FavoritesController : Controller
    {
        private readonly OnlineCateringContext _context;

        public FavoritesController(OnlineCateringContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customerId = GetCustomerIdFromSession();
            var favorites = await _context.FavoriteCaterers
                .Where(f => f.CustomerId == customerId)
                .Include(f => f.Caterer)
                .ToListAsync();

            return View(favorites);
        }

        public async Task<IActionResult> AddToFavorites(int catererId)
        {
            var customerId = GetCustomerIdFromSession();
            var favorite = new FavoriteCaterer
            {
                CustomerId = customerId,
                CatererId = catererId
            };
            _context.FavoriteCaterers.Add(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromFavorites(int favoriteId)
        {
            var favorite = await _context.FavoriteCaterers.FindAsync(favoriteId);
            if (favorite != null)
            {
                _context.FavoriteCaterers.Remove(favorite);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
*/