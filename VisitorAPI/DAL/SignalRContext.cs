

using Microsoft.EntityFrameworkCore;

namespace VisitorAPI.DAL
{
    public class SignalRContext : DbContext
    {
        public SignalRContext(DbContextOptions<SignalRContext> opts) : base(opts)
        {
            
        }
        public DbSet<Visitor> visitors { get; set; }
    }
}
