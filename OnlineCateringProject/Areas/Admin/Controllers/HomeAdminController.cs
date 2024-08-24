using Microsoft.AspNetCore.Mvc;
using OnlineCateringProject.Models.Authentication;

namespace OnlineCateringProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HomeAdminController : Controller
    {
        [Route("/admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
