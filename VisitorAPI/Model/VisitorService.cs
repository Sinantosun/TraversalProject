using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VisitorAPI.DAL;
using VisitorAPI.Hubs;

namespace VisitorAPI.Model
{
    public class VisitorService
    {
        private readonly SignalRContext _context;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorService(SignalRContext postgreDBSQLContext, IHubContext<VisitorHub> hubContext)
        {
            _context = postgreDBSQLContext;
            _hubContext = hubContext;
        }

        public IQueryable<Visitor> GetList()
        {
            return _context.visitors.AsQueryable();
        }

        public async Task SaveVisitor(Visitor visitor)
        {
            await _context.visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReciveVisitorList", getVisitorChartList());
        }

        public List<VisitorChart> getVisitorChartList()
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select tarih,[1],[2],[3],[4],[5] from (select[City],CityVisitCount,Cast([VisitDate] as Date) as tarih from visitors) as visitTable pivot (sum(CityVisitCount) For City in([1],[2],[3],[4],[5])) as pivottable order by tarih asc";
                command.CommandType = System.Data.CommandType.Text;
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VisitorChart visitorChart = new VisitorChart();
                        visitorChart.VisitDate = reader.GetDateTime(0).ToShortDateString();
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (DBNull.Value.Equals(reader[x]))
                            {
                                visitorChart.Counts.Add(0);
                            }
                            else
                            {
                                visitorChart.Counts.Add(reader.GetInt32(x));
                            }
                         
                        });
                        visitorCharts.Add(visitorChart);

                    }
                }
                _context.Database.CloseConnection();
                return visitorCharts;

            };
        }
    }
}
