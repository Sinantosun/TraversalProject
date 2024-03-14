using MediatR;
using TraversalProject.CQRS.Results.GuideResults;

namespace TraversalProject.CQRS.Queries.GuideQueries
{
    public class GetGuideByIDQuery : IRequest<GetGuideByIDQueryResult>
    {
        public GetGuideByIDQuery(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
