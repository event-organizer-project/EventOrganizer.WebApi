using EventOrganizer.EF.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new ModelConfigurations.TagToEventConfiguration());
            modelBuilder.ApplyConfiguration(new EventTagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            InitialDataSeeding.Filling(modelBuilder);
        }
    }
}
