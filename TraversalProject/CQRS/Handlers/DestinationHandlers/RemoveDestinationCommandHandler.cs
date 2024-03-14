using DataAccsesLayer.Concrete;
using TraversalProject.CQRS.Commands.DestinationCommands;

namespace TraversalProject.CQRS.Handlers.DestinationHandlers
{
    public class RemoveDestinationCommandHandler
    {
        private readonly Context _context;

        public RemoveDestinationCommandHandler(Context context)
        {
            _context = context;
        }
        public void Handle(RemoveDestinationCommand command)
        {
            var values = _context.Destinations.Find(command.id);

            _context.Destinations.Remove(values);
            _context.SaveChanges();
        }
    }
}
