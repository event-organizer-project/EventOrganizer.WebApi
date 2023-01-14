using EventOrganizer.Domain.Models;
using EventOrganizer.EF.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF
{
    public class EventOrganazerDbContext : DbContext
    {
        public DbSet<EventModel> Events { get; set; }

        public EventOrganazerDbContext(DbContextOptions<EventOrganazerDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventModelConfiguration());
        }
    }
}
