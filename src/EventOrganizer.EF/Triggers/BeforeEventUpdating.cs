using EntityFrameworkCore.Triggered;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.Triggers
{
    public class BeforeEventUpdating : IBeforeSaveTrigger<EventModel>
    {
        readonly EventOrganazerDbContext dbContext;

        public BeforeEventUpdating(EventOrganazerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task BeforeSave(ITriggerContext<EventModel> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Modified)
            {
                foreach (var tagToEvent in context.Entity.TagToEvents)
                {
                    if (!dbContext.EventTags.Any(tag => tag.Keyword == tagToEvent.Keyword))
                        dbContext.EventTags.Add(new EventTag { Keyword = tagToEvent.Keyword });
                }
            }

            return Task.CompletedTask;
        }
    }
}
