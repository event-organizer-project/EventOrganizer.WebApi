namespace EventOrganizer.Domain.Models
{
    public class DialogueMessage
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateTime { get; set; }

        public virtual User Sender { get; set; }

        public virtual EventModel Event { get; set; }
    }
}
