using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class UpdateEventCommand : ICommand<UpdateEventCommandParameters, EventDetailDTO>
    {
        private readonly IEventRepository eventRepository;

        public readonly IMapper mapper;

        public UpdateEventCommand(IEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }
        public EventDetailDTO Execute(UpdateEventCommandParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var eventModel = eventRepository.Get(parameters.EventDetailDTO.Id);

            eventModel = mapper.Map(parameters.EventDetailDTO, eventModel);

            var result = eventRepository.Update(eventModel);

            return mapper.Map<EventDetailDTO>(result);
        }
    }
}
