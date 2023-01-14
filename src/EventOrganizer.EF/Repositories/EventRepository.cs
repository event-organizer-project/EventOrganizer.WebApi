using EventOrganizer.Core.Infrastructure;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventOrganizer.EF.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public EventRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext;
        }

        public IList<EventModel> GetEventList(EventListSettings eventListSettings)
        {
            throw new NotImplementedException();
        }

        public EventModel Get(int id)
        {
            throw new NotImplementedException();

            //var model = dbcontext.events.firstordefault(x => x.id == id);

            //return model;
        }

        public EventModel Create(EventModel eventModel)
        {
            return new EventModel();
            throw new NotImplementedException();
        }

        public EventModel Update(EventModel eventModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
