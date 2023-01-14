namespace EventOrganizer.Domain.Models
{
    public class DialogueMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public User Sender { get; set; }

        public EventModel Event { get; set; }
    }
}
