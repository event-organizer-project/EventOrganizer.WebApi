using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class DeleteEventCommand : ICommand<DeleteEventCommandParameters, VoidResult>
    {
        private readonly IEventRepository eventRepository;

        public DeleteEventCommand(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public VoidResult Execute(DeleteEventCommandParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            eventRepository.Delete(parameters.EventId);

            return new VoidResult();
        }
    }
}
