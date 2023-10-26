using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public TagRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        public IEnumerable<EventTag> GetAll()
        {
            return dbContext.EventTags;
        }
    }
}
