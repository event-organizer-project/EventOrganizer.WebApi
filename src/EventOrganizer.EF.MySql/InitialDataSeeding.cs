using EventOrganizer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.EF.MySql
{
    internal class InitialDataSeeding
    {
        public static void Filling(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
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


            modelBuilder.Entity<OnlineEvent>().HasData(new OnlineEvent
            {
                Id = 1,
                Title = "Event organizer presentation",
                Description = "Mastery completion and presentation of the final product",
                StartDate = new DateTimeOffset(2024, 1, 24, 12, 30, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2024, 1, 24, 14, 30, 0, TimeSpan.Zero),
                OwnerId = 1
            }, new OnlineEvent
            {
                Id = 2,
                Title = "Event created by John",
                Description = "Description created by John",
                StartDate = new DateTimeOffset(2024, 1, 26, 18, 0, 0, TimeSpan.Zero),
                EndDate = new DateTimeOffset(2024, 1, 26, 20, 0, 0, TimeSpan.Zero),
                OwnerId = 2
            });


            modelBuilder.Entity<TagToEvent>().HasData(
                new TagToEvent { Keyword = "godel", EventId = 1 },
                new TagToEvent { Keyword = "online", EventId = 1 },
                new TagToEvent { Keyword = "godel", EventId = 2 });

            modelBuilder.Entity<EventInvolvement>().HasData(
                new EventInvolvement { UserId = 1, EventId = 1 },
                new EventInvolvement { UserId = 2, EventId = 2 });
        }
    }
}
