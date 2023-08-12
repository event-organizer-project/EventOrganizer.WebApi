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
                StartDate = new DateTime(2023, 5, 28),
                EndDate = new DateTime(2023, 5, 28),
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(20, 0, 0),
                OwnerId = 1
            }, new OnlineEvent
            {
                Id = 2,
                Title = "Event created by John",
                Description = "Description created by John",
                StartDate = new DateTime(2023, 8, 11),
                EndDate = new DateTime(2023, 8, 11),
                StartTime = new TimeSpan(15, 0, 0),
                EndTime = new TimeSpan(17, 0, 0),
                OwnerId = 2
            });


            modelBuilder.Entity<TagToEvent>().HasData(
                new TagToEvent { Keyword = "godel", EventId = 1 },
                new TagToEvent { Keyword = "online", EventId = 1 },
                new TagToEvent { Keyword = "godel", EventId = 2 }
                );
        }
    }
}
