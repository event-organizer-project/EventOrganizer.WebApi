using System;

namespace EventOrganizer.Domain.Models
{
    // Optional entity for the event model, in the future it can store some attachments, docs, images
    public class EventResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public virtual EventModel Event { get; set; }
    }
}
