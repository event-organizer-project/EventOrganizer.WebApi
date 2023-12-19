using EventOrganizer.EF;

namespace EventOrganizer.WebApi.Services
{
    public class HealthService : IHealthService
    {
        private readonly EventOrganazerDbContext eventOrganazerDbContext;

        public HealthService(EventOrganazerDbContext eventOrganazerDbContext)
        {
            this.eventOrganazerDbContext = eventOrganazerDbContext
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        public bool IsDbConnectionHealthy()
        {
            return eventOrganazerDbContext.Database.CanConnect();
        }
    }
}
