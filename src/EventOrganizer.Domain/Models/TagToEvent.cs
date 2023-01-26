namespace EventOrganizer.Domain.Models
{
    public class TagToEvent
    {
        public int EventId { get; set; }

        public virtual EventModel EventModel { get; set; }

        public string Keyword { get; set; }

        public virtual EventTag EventTag { get; set; }
    }
}
