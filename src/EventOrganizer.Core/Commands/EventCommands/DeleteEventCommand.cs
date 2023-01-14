using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;
using System;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class DeleteEventCommand : ICommand<DeleteEventCommandParameters, EventModel>
    {
        private readonly IEventRepository eventRepository;

        public DeleteEventCommand(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public EventModel Execute(DeleteEventCommandParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
