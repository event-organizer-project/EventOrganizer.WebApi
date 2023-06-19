using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql.Repositories
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
                .Include(e => e.EventTags);
        }

        public EventModel Get(int id)
        {
            var model = dbContext.EventModels
                .Include(e => e.Owner)
                .Include(e => e.Members)
                .Include(e => e.EventTags)
                .Include(e => e.EventResults).FirstOrDefault(x => x.Id == id);

            return model;
        }

        public EventModel Create(EventModel eventModel)
        {
            dbContext.Attach(eventModel.Owner);

            dbContext.EventModels.Add(eventModel);

            dbContext.SaveChanges();

            return GetAll().First(x => x.Id == eventModel.Id);
        }

        public EventModel Update(EventModel eventModel)
        {
            dbContext.Update(eventModel);

            dbContext.SaveChanges();

            var result = Get(eventModel.Id);

            result.EventTags = result.TagToEvents.Select(x => x.EventTag).ToArray();

            return result;
        }

        public void Delete(int id)
        {
            var eventModel = dbContext.EventModels
                .Include(x => x.EventTags)
                .ThenInclude(x => x.TagToEvents)
                .FirstOrDefault(x => x.Id == id);

            // TO DO: chanhe logic
            if (eventModel == null)
                throw new ArgumentException();

            dbContext.EventModels.Remove(eventModel);

            dbContext.SaveChanges();
        }
    }
}
