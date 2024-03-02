using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.AdminDashboard
{
    public class _AdminDashboardHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
