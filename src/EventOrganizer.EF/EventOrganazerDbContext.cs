using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EventOrganizer.EF
{
    public class EventOrganazerDbContext : DbContext
    {
        public DbSet<EventModel> EventModels { get; set; }
        public DbSet<OnlineEvent> OnlineEvents { get; set; }
        public DbSet<OfflineEvent> OfflineEvents { get; set; }

        public DbSet<EventTag> EventTags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventResult> EventResults { get; set; }

        public DbSet<DialogueMessage> DialogueMessages { get; set; }

        public DbSet<EventInvolvement> EventInvolvements { get; set; }

        public DbSet<TagToEvent> TagToEvents { get; set; }

        public EventOrganazerDbContext(DbContextOptions<EventOrganazerDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
