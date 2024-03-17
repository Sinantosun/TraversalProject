using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VisitorAPI.DAL;
using VisitorAPI.Hubs;

namespace VisitorAPI.Model
{
    public class VisitorService
    {
        private readonly PostgreDBSQLContext _context;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorService(PostgreDBSQLContext postgreDBSQLContext, IHubContext<VisitorHub> hubContext)
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
            await _hubContext.Clients.All.SendAsync("ReciveVisitorList", "aa");
        }

        public List<VisitorChart> getVisitorChartList()
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM crosstab 'SELECT VisitDate,City, CityVisitCount FROM visitors ORDER BY 1, 2') AS ct (VisitDate DATE, City1 INT, City2, INT, City3 INT, City4 INT, City5 INT);";
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
                            visitorChart.Counts.Add(reader.GetInt32(x));
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
