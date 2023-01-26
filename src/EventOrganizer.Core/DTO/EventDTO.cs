using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public ICollection<string> EventTags { get; set; }
    }
}
