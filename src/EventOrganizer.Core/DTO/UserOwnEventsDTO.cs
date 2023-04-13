namespace EventOrganizer.Core.DTO
{
    public class UserOwnEventsDTO
    {
        public IList<EventDTO> CreatedEvents  { get; set; } = new List<EventDTO>();

        public IList<EventDTO> JoinedEvents { get; set; } = new List<EventDTO>();
    }
}
