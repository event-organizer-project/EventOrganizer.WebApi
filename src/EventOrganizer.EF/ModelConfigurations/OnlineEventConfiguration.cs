using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.ModelConfigurations
{
    public class OnlineEventConfiguration : IEntityTypeConfiguration<OnlineEvent>
    {
        public void Configure(EntityTypeBuilder<OnlineEvent> builder)
        {
            // Initial data seeding
            builder.HasData(new OnlineEvent
            {
                Id = 1,
                Title = "Event organizer presentation",
                Description = "Mastery completion and presentation of the final product",
                StartDate = new DateTime(2023, 5, 28),
                EndDate = new DateTime(2023, 5, 28),
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(20, 0, 0),
                OwnerId = 1
            });
        }
    }
}
