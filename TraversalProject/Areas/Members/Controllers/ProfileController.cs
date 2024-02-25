using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Members.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
