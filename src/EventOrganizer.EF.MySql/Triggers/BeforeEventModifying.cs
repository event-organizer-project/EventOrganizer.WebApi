﻿using EntityFrameworkCore.Triggered;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Triggers
{
    public class BeforeEventModifying : IBeforeSaveTrigger<EventModel>
    {
        readonly EventOrganazerDbContext dbContext;

        public BeforeEventModifying(EventOrganazerDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task BeforeSave(ITriggerContext<EventModel> context, CancellationToken cancellationToken)
        {
            if ((context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
                && context.Entity.TagToEvents != null)
            {
                foreach (var tagToEvent in context.Entity.TagToEvents)
                {
                    if (!dbContext.EventTags.Any(tag => tag.Keyword == tagToEvent.Keyword))
                        dbContext.EventTags.Add(new EventTag { Keyword = tagToEvent.Keyword });
                }
            }

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
