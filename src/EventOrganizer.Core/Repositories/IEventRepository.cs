using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventModel> GetAll();

        EventModel? Get(int id);

        EventModel Create(EventModel eventModel);

        EventModel Update(EventModel eventModel);

        EventModel Delete(int id);
    }
}
