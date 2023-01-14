namespace EventOrganizer.Domain.Models
{
    public class EventTag
    {
        public string Keyword { get; set; }

        public ICollection<EventModel> EventModels { get; set; }

        public ICollection<TagToEvent> TagToEvents { get; set; }
    }
}
