namespace EventOrganizer.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        public string Endpoint { get; set; }

        public string P256DH { get; set; }

        public string Auth { get; set; }

        public int UserId { get; set; }
    }
}
