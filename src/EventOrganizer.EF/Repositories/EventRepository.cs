using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public EventRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext;
        }

        public IEnumerable<EventModel> GetAll()
        {
            return dbContext.EventModels;
        }

        public EventModel Get(int id)
        {
            var model = dbContext.EventModels.FirstOrDefault(x => x.Id == id);

            return model;
        }

        public EventModel Create(EventModel eventModel)
        {
            dbContext.EventModels.Add(eventModel);

            return eventModel;
        }

        public EventModel Update(EventModel eventModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var eventModel = dbContext.EventModels.FirstOrDefault(x => x.Id == id);

            if (eventModel == null)
                throw new ArgumentException();

            dbContext.EventModels.Remove(eventModel);
        }
    }
}
