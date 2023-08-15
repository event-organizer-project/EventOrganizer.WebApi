using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.DTO
{
    public class EventDetailDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public RecurrenceType RecurrenceType { get; set; }

        public ICollection<string> EventTags { get; set; } = new List<string>();

        public UserDTO? Owner { get; set; }

        public ICollection<UserDTO> Members { get; set; } = new List<UserDTO>();
    }
}
