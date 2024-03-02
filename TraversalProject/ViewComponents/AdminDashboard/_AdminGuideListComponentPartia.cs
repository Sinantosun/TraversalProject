using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.AdminDashboard
{
    public class _AdminGuideListComponentPartia : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
