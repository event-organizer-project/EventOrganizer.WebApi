using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql.ModelConfigurations
{
    internal class EventModelConfiguration : IEntityTypeConfiguration<EventModel>
    {
        public void Configure(EntityTypeBuilder<EventModel> builder)
        {
            builder.Property(em => em.Title)
                .HasMaxLength(100);

            builder.Property(em => em.RecurrenceType)
                    .HasDefaultValue(RecurrenceType.DoesNotRepeat);

            builder.Property(em => em.IsMessagingAllowed)
                    .HasDefaultValue(false);
        }
    }
}
