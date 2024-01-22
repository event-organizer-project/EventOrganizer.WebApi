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

            builder.Property(em => em.StartDate)
                .HasColumnType("DATETIME(6)")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.Property(em => em.EndDate)
                .HasColumnType("DATETIME(6)")
                .HasDefaultValueSql("(DATE_ADD(CURRENT_TIMESTAMP(6), INTERVAL 2 HOUR))");

            builder.Property(em => em.LastEndDate)
                .HasColumnType("DATETIME(6)");

            builder.Property(em => em.RecurrenceType)
                .HasDefaultValue(RecurrenceType.DoesNotRepeat);

            builder.Property(em => em.IsMessagingAllowed)
                .HasDefaultValue(false);
        }
    }
}
