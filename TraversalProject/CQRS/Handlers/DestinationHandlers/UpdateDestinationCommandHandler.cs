using DataAccsesLayer.Concrete;
using TraversalProject.CQRS.Commands.DestinationCommands;

namespace TraversalProject.CQRS.Handlers.DestinationHandlers
{
    public class UpdateDestinationCommandHandler
    {
        private readonly Context _context;

        public UpdateDestinationCommandHandler(Context context)
        {
            _context = context;
        }

        public void Handle(UpdateDestinationCommand command)
        {
            var values = _context.Destinations.Find(command.destinationID);
            values.City = command.city;
            values.DayNight = command.daynight;
            values.Price = command.price;
            _context.SaveChanges();
        }
    }
}
