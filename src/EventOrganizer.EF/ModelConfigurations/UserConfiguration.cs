using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventOrganizer.EF.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
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

            // Initial data seeding
            builder.HasData(new User
            {
                Id = 1,
                FirstName = "Mikita",
                LastName = "N",
                Nickname = "mikita.n",
                Email = "mikita.n@godeltech.com"
            }, new User
            {
                Id = 2,
                FirstName = "John",
                LastName = "Doe",
                Nickname = "john.doe",
                Email = "john.doe@gmail.com"
            });
        }
    }
}
