using EntityFrameworkCore.Triggered;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.Triggers
{
    public class BeforeEventAdding : IBeforeSaveTrigger<EventModel>
    {
        readonly EventOrganazerDbContext dbContext;

        public BeforeEventAdding(EventOrganazerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task BeforeSave(ITriggerContext<EventModel> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)
            {
                foreach(var tagToEvent in context.Entity.TagToEvents)
                {
                    if (!dbContext.EventTags.Any(tag => tag.Keyword == tagToEvent.Keyword))
                        dbContext.EventTags.Add(new EventTag { Keyword = tagToEvent.Keyword });
                }
            }

            return Task.CompletedTask;
        }
    }
}
