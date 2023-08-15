using EventOrganizer.Core.DTO;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class UpdateEventCommandParameters
    {
        public UpdateEventCommandParameters(EventDetailDTO eventDetailDTO)
        {
            EventDetailDTO = eventDetailDTO;
        }

        public EventDetailDTO EventDetailDTO { get; set; }
    }
}
