namespace EventOrganizer.Core.DTO
{
    public class DaylyScheduleDTO
    {
        public string WeekDay { get; set; }

        public DateTimeOffset Date { get; set; }
        
        public EventDTO[] Events { get; set; }
    }
}
