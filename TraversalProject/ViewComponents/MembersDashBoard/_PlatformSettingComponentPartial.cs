using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.MembersDashBoard
{
    public class _PlatformSettingComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
