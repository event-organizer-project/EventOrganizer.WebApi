using EventOrganizer.Core.DTO;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class CreateEventCommandParameters
    {
        public CreateEventCommandParameters(EventDetailDTO eventDetailDTO)
        {
            EventDetailDTO = eventDetailDTO;
        }

        public EventDetailDTO EventDetailDTO { get; set; }
    }
}
