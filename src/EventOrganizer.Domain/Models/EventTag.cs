namespace EventOrganizer.Domain.Models
{
    public class EventTag
    {
        public string Keyword { get; set; }

        public virtual ICollection<EventModel> EventModels { get; set; }

        public virtual ICollection<TagToEvent> TagToEvents { get; set; }
    }
}
