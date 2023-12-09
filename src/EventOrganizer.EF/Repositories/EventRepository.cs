using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public EventRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext 
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        public IEnumerable<EventModel> GetAll()
        {
            return dbContext.EventModels
                .Include(e => e.Owner)
                .Include(e => e.Members)
                .Include(e => e.EventTags)
                .Include(e => e.EventResults);
        }

        public EventModel? Get(int id)
        {
            var model = GetAll().FirstOrDefault(x => x.Id == id);

            return model;
        }

        public EventModel Create(EventModel eventModel)
        {
            dbContext.EventModels.Add(eventModel);

            dbContext.SaveChanges();

            return eventModel;
        }

        public EventModel Update(EventModel eventModel)
        {
            dbContext.Update(eventModel);

            dbContext.SaveChanges();

            return Get(eventModel.Id);
        }

        public EventModel Delete(int id)
        {
            var eventModel = dbContext.EventModels.FirstOrDefault(x => x.Id == id);

            // TO DO: chanhe logic
            if (eventModel == null)
                throw new ArgumentException();

            dbContext.EventModels.Remove(eventModel);

            dbContext.SaveChanges();

            return eventModel;
        }
    }
}
