using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.ViewComponents.MembersDashBoard
{
    public class _ProfileInformationComponentPartial :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
