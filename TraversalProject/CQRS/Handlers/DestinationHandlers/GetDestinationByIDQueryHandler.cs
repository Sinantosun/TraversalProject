using DataAccsesLayer.Concrete;
using TraversalProject.CQRS.Queries.DestinationQueries;
using TraversalProject.CQRS.Results.DestinationResults;

namespace TraversalProject.CQRS.Handlers.DestinationHandlers
{
    public class GetDestinationByIDQueryHandler
    {
        private readonly Context _context;

        public GetDestinationByIDQueryHandler(Context context)
        {
            _context = context;
        }
        public GetDestinationByIDQueryResult Handle(GetDestinationByIDQuery query)
        {
            var values = _context.Destinations.Find(query.id);
            return new GetDestinationByIDQueryResult()
            {
                city = values.City,
                daynight = values.DayNight,
                destinationID = values.DestinationID,
                price=values.Price

            };
        }
    }
}
