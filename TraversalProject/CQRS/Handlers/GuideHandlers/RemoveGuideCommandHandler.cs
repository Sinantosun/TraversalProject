using DataAccsesLayer.Concrete;
using MediatR;
using TraversalProject.CQRS.Commands.GuideCommands;

namespace TraversalProject.CQRS.Handlers.GuideHandlers
{
    public class RemoveGuideCommandHandler : IRequestHandler<RemoveGuideCommand>
    {
        private readonly Context _context;

        public RemoveGuideCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveGuideCommand request, CancellationToken cancellationToken)
        {
            var values = _context.Guides.Find(request.id);
            _context.Guides.Remove(values);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
