namespace EventOrganizer.Domain.Models
{
    public class TagToEvent
    {
        public int EventId { get; set; }

        public EventModel EventModel { get; set; }

        public string Keyword { get; set; }

        public EventTag EventTag { get; set; }
    }
}
