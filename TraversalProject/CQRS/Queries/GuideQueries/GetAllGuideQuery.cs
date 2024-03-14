using MediatR;
using TraversalProject.CQRS.Results.GuideResults;

namespace TraversalProject.CQRS.Queries.GuideQueries
{
    public class GetAllGuideQuery:IRequest<List<GetAllGuideQueryResult>>
    {
    }
}
