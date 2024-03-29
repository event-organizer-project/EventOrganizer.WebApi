﻿namespace EventOrganizer.Domain.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        // Event Time Settings
        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public DateTimeOffset? LastEndDate { get; set; }

        public RecurrenceType RecurrenceType { get; set; }
        // Event Time Settings

        public virtual ICollection<EventTag> EventTags { get; set; }

        public virtual ICollection<TagToEvent> TagToEvents { get; set; }

        public int OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<EventInvolvement> EventInvolvements { get; set; }

        public virtual ICollection<EventResult> EventResults { get; set; }

        public bool IsMessagingAllowed { get; set; }

        public virtual ICollection<DialogueMessage> DialogueMessages { get; set; }
    }
}
