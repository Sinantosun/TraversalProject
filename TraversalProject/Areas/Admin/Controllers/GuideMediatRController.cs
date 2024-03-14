using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.CQRS.Commands.GuideCommands;
using TraversalProject.CQRS.Queries.GuideQueries;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class GuideMediatRController : Controller
    {
        private readonly IMediator _mediator;

        public GuideMediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _mediator.Send(new GetAllGuideQuery());
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuides(int id)
        {
            var values = await _mediator.Send(new GetGuideByIDQuery(id));
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> GetGuides(UpdateGuideCommand updateGuideCommand)
        {
            await _mediator.Send(updateGuideCommand);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGuide(CreateGuideCommand command)
        {
            await _mediator.Send(command);
            ViewBag.Result = "Yeni Rehber Eklendi.";
            return View();
        }

        public async Task<IActionResult> DeleteGuide(int id)
        {
            await _mediator.Send(new RemoveGuideCommand(id));
            return RedirectToAction("Index");
        }
    }
}
