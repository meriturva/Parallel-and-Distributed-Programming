using EventsOutOfProcessByDatabaseShared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventsOutOfProcessByDatabaseShared
{
    public class EventBusContext : DbContext
    {
        public EventBusContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
        }
    }
}
