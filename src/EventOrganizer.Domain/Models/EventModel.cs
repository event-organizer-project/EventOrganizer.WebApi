using System;
using System.Collections.Generic;

namespace EventOrganizer.Domain.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

        public IList<EventTag> EventTags { get; set; }
    }
}
