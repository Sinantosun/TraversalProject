using DataAccsesLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.AdminDashboard
{
    public class _Card1StatisticComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            ViewBag.v1 = context.Destinations.Count();
            ViewBag.v2 = context.Users.Count();
            return View();
        }
    }
}
