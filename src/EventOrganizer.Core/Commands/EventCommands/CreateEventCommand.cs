using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class CreateEventCommand : ICommand<CreateEventCommandParameters, EventDetailDTO>
    {
        private readonly IEventRepository eventRepository;

        private readonly IMapper mapper;

        private readonly IUserHandler userHandler;

        public CreateEventCommand(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
        }

        public EventDetailDTO Execute(CreateEventCommandParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var eventModel = mapper.Map<EventModel>(parameters.EventDetailDTO);

            eventModel.Owner = userHandler.GetCurrentUser();

            var result = eventRepository.Create(eventModel);

            return mapper.Map<EventDetailDTO>(result);
        }
    }
}
