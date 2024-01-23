using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public ICollection<string> EventTags { get; set; } = new List<string>();
    }
}
