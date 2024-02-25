using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Members.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
