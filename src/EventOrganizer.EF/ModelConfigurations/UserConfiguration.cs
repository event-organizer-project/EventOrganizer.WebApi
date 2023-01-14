using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventOrganizer.EF.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);

            builder
                .HasMany(u => u.CreatedEvents)
                .WithOne(em => em.Owner);

            builder
                .HasMany(u => u.TrackedEvents)
                .WithMany(em => em.Members)
                .UsingEntity<EventInvolvement>();            
        }
    }
}
