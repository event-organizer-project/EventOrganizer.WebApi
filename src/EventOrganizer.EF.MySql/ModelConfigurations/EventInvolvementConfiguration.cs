using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql.ModelConfigurations
{
    public class EventInvolvementConfiguration : IEntityTypeConfiguration<EventInvolvement>
    {
        public void Configure(EntityTypeBuilder<EventInvolvement> builder)
        {
            builder.Property(ei => ei.JoiningDate).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder
                .HasOne(ei => ei.EventModel)
                .WithMany(em => em.EventInvolvements)
                .HasForeignKey(ei => ei.EventId);

            builder
                .HasOne(ei => ei.User)
                .WithMany(u => u.EventInvolvements)
                .HasForeignKey(ei => ei.UserId);

            builder.
                HasKey(ei => new { ei.EventId, ei.UserId });

            builder.ToTable(nameof(EventInvolvement));
        }
    }
}
