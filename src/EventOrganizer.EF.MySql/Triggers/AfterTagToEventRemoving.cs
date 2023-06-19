using EntityFrameworkCore.Triggered;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Triggers
{
    public class AfterTagToEventRemoving : IAfterSaveTrigger<TagToEvent>
    {
        readonly EventOrganazerDbContext dbContext;

        public AfterTagToEventRemoving(EventOrganazerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task AfterSave(ITriggerContext<TagToEvent> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                var tagsForDeleting = dbContext.EventTags.Where(x => x.TagToEvents.Count == 0);

                dbContext.EventTags.RemoveRange(tagsForDeleting);
            }

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
