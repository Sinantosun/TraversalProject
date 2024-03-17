

using Microsoft.EntityFrameworkCore;

namespace VisitorAPI.DAL
{
    public class PostgreDBSQLContext : DbContext
    {
        public PostgreDBSQLContext(DbContextOptions<PostgreDBSQLContext> opts) : base(opts)
        {
            
        }
        public DbSet<Visitor> visitors { get; set; }
    }
}
