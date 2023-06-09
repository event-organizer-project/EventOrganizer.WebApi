using EventOrganizer.EF.EntityConfigurations;
using EventOrganizer.EF.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql
{
    public class EventOrganazerMySqlDbContext : EventOrganazerDbContext
    {
        public EventOrganazerMySqlDbContext(DbContextOptions<EventOrganazerMySqlDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModelConfigurations.EventModelConfiguration());
            modelBuilder.ApplyConfiguration(new ModelConfigurations.EventInvolvementConfiguration());
            modelBuilder.ApplyConfiguration(new EventTagConfiguration());
            modelBuilder.ApplyConfiguration(new OnlineEventConfiguration());
            modelBuilder.ApplyConfiguration(new TagToEventConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
