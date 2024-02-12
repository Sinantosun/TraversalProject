using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.VievComponents._UILayoutComponents
{
    public class _UIHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
