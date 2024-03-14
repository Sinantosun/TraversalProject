using MediatR;

namespace TraversalProject.CQRS.Commands.GuideCommands
{
    public class RemoveGuideCommand : IRequest
    {
        public RemoveGuideCommand(int id)
        {
            this.id = id;
        }

        public int id { get; set; }
    }
}
