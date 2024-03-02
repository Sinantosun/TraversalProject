using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.AdminDashboard
{
    public class _TotalRevuneComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
