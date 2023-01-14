using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using System;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class CreateEventCommand : ICommand<CreateEventCommandParameters, EventModel>
    {
        private readonly IEventRepository eventRepository;

        public CreateEventCommand(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public EventModel Execute(CreateEventCommandParameters parameters)
        {
            /* validate parameters */

            var result = eventRepository.Create(parameters.EventModel);

            return result;
        }
    }
}
