namespace EventOrganizer.Domain.Models
{
    public class EventInvolvement
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int EventId { get; set; }

        public EventModel EventModel { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime? LeavingDate { get; set; }
    }
}
