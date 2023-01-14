using EventOrganizer.Core.Infrastructure;
using EventOrganizer.Domain.Models;
using System.Collections.Generic;

namespace EventOrganizer.Core.Repositories
{
    public interface IEventRepository
    {
        IList<EventModel> GetEventList(EventListSettings eventListSettings);

        EventModel Get(int id);

        EventModel Create(EventModel eventModel);

        EventModel Update(EventModel eventModel);

        void Delete(int id);
    }
}
