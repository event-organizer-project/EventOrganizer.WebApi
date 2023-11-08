using AutoMapper;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class ScheduleEventCommand : ICommand<ScheduleEventCommandParameters, EventDetailDTO>
    {
        private readonly IEventRepository eventRepository;

        private readonly IMapper mapper;

        private readonly IUserHandler userHandler;

        private readonly ISchedulerClient schedulerClient;

        public ScheduleEventCommand(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler, ISchedulerClient schedulerClient)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
            this.schedulerClient = schedulerClient
                ?? throw new ArgumentNullException(nameof(schedulerClient));
        }

        public EventDetailDTO Execute(ScheduleEventCommandParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var eventModel = eventRepository.Get(parameters.EventId);

            if (eventModel == null)
                throw new ResourceNotFoundException();

            var currentUser = userHandler.GetCurrentUser();

            if(parameters.IsEventScheduled)
                eventModel.Members.Add(currentUser);
            else
            {
                var userForDelete = eventModel.Members.FirstOrDefault(x => x.Id == currentUser.Id);
                eventModel.Members.Remove(userForDelete);
            }
                
            var result = eventRepository.Update(eventModel);

            if (result.StartDate == DateTime.Today)
            {
                var schedulerResult = parameters.IsEventScheduled
                    ? schedulerClient.AddEventToSchedule(result.Id, currentUser.Id)
                    : schedulerClient.RemoveEventFromSchedule(result.Id, currentUser.Id);
            }

            return mapper.Map<EventDetailDTO>(result);
        }
    }
}
