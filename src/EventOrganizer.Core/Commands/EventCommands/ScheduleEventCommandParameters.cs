namespace EventOrganizer.Core.Commands.EventCommands
{
    public class ScheduleEventCommandParameters
    {
        public int EventId { get; set; }
        public bool IsEventScheduled { get; set; }
    }
}
