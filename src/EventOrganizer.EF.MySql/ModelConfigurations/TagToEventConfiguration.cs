using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql.ModelConfigurations
{
    internal class TagToEventConfiguration : IEntityTypeConfiguration<TagToEvent>
    {
        public void Configure(EntityTypeBuilder<TagToEvent> builder)
        {
            builder
                .HasOne(tte => tte.EventModel)
                .WithMany(em => em.TagToEvents)
                .HasForeignKey(tte => tte.EventId);

            builder
                .HasOne(tte => tte.EventTag)
                .WithMany(et => et.TagToEvents)
                .HasForeignKey(tte => tte.Keyword);

            builder.
                HasKey(tte => new { tte.EventId, tte.Keyword });

            builder.ToTable(nameof(TagToEvent));
        }
    }
}
