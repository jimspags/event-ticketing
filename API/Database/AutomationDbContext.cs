using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class AutomationDbContext : DbContext
    {
        public AutomationDbContext(DbContextOptions<AutomationDbContext> options) : base (options)
        {
            
        }

        public DbSet<Event> Events { get; set; }
    }
}
