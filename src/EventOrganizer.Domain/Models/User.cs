namespace EventOrganizer.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<EventModel> CreatedEvents { get; set; }

        public virtual ICollection<EventModel> TrackedEvents { get; set; }

        public virtual ICollection<EventInvolvement> EventInvolvements { get; set; }
    }
}
