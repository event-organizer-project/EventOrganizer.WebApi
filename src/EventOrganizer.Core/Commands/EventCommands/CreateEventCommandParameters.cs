using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class CreateEventCommandParameters
    {
        public EventModel EventModel { get; set; }
        /* other parameters*/
    }
}
