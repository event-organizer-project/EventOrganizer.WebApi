using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public SubscriptionRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        public Subscription Create(Subscription subscription)
        {
            dbContext.Subscriptions.Add(subscription);

            dbContext.SaveChanges();

            return subscription;
        }
    }
}
