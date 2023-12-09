using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class DeleteEventCommand : ICommand<DeleteEventCommandParameters, VoidResult>
    {
        private readonly IEventRepository eventRepository;

        private readonly ISchedulerClient schedulerClient;

        public DeleteEventCommand(IEventRepository eventRepository, ISchedulerClient schedulerClient)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            this.schedulerClient = schedulerClient ?? throw new ArgumentNullException(nameof(schedulerClient));
        }

        public VoidResult Execute(DeleteEventCommandParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var result = eventRepository.Delete(parameters.EventId);

            if (result.StartDate == DateTime.Today)
            {
                schedulerClient.RemoveEventFromSchedule(result.Id);
            }

            return new VoidResult();
        }
    }
}
