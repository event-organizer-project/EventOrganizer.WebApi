using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Repositories
{
    public interface ISubscriptionRepository
    {
        Subscription Create(Subscription subscription);
    }
}
