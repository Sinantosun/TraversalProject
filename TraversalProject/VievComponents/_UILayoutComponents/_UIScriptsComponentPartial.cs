using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.VievComponents._UILayoutComponents
{
    public class _UIScriptsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
