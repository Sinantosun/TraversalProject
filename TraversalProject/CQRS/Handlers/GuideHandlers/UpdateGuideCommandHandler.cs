using DataAccsesLayer.Concrete;
using MediatR;
using TraversalProject.CQRS.Commands.GuideCommands;

namespace TraversalProject.CQRS.Handlers.GuideHandlers
{
    public class UpdateGuideCommandHandler : IRequestHandler<UpdateGuideCommand>
    {
        private readonly Context _context;

        public UpdateGuideCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateGuideCommand request, CancellationToken cancellationToken)
        {

            var values = _context.Guides.Find(request.GuideID);
            values.Name = request.Name;
            values.Description = request.Description;
            await _context.SaveChangesAsync();

            return Unit.Value;

        }
    }
}
