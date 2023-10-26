using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<EventTag> GetAll();
    }
}
