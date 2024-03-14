using DataAccsesLayer.Concrete;
using EntityLayer.Concrete;
using MediatR;
using TraversalProject.CQRS.Commands.GuideCommands;

namespace TraversalProject.CQRS.Handlers.GuideHandlers
{
    public class CreateGuideCommandHandler : IRequestHandler<CreateGuideCommand>
    {
        private readonly Context _context;

        public CreateGuideCommandHandler(Context context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateGuideCommand request, CancellationToken cancellationToken)
        {
            _context.Guides.Add(new Guide {
              
                Name=request.Name,
                Status=true,
                Description=request.Description,
                Description2="test",
                GuideListImage="test",
                Image="test",
                InstagramUrl="test",
                TwitterUrl="test",

                
            
            });
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    } 
}
