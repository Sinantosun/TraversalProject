using Microsoft.AspNetCore.SignalR;
using VisitorAPI.Model;

namespace VisitorAPI.Hubs
{
    public class VisitorHub : Hub
    {
        private readonly VisitorService _visitorService;

        public VisitorHub(VisitorService visitorService)
        {
            _visitorService = visitorService;
        }
        public async Task getVisitorList()
        {
            await Clients.All.SendAsync("ReciveVisitList",_visitorService.getVisitorChartList());
        }
    }
}
