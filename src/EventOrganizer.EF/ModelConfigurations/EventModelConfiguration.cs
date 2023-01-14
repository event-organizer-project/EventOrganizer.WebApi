using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventOrganizer.EF.ModelConfigurations
{
    internal class EventModelConfiguration : IEntityTypeConfiguration<EventModel>
    {
        public void Configure(EntityTypeBuilder<EventModel> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            /* other settings */
        }
    }
}
