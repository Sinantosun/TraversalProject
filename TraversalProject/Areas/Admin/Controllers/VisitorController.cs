using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
