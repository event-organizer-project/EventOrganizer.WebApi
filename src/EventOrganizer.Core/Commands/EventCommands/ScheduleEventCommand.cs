using AutoMapper;
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

        public ScheduleEventCommand(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
        }

        public EventDetailDTO Execute(ScheduleEventCommandParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var eventModel = eventRepository.Get(parameters.EventId);

            var currentUser = userHandler.GetCurrentUser();

            if(parameters.IsEventScheduled)
                eventModel.Members.Add(currentUser);
            else
            {
                var userForDelete = eventModel.Members.FirstOrDefault(x => x.Id == currentUser.Id);
                eventModel.Members.Remove(userForDelete);
            }
                
            var result = eventRepository.Update(eventModel);

            return mapper.Map<EventDetailDTO>(result);
        }
    }
}
