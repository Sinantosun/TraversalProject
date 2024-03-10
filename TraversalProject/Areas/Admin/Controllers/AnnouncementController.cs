using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
