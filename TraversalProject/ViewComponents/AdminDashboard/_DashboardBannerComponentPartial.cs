using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.AdminDashboard
{
    public class _DashboardBannerComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
