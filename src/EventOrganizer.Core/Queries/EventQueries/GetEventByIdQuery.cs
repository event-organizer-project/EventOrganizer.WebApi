using AutoMapper;
using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;

namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetEventByIdQuery : IQuery<GetEventByIdQueryParameters, EventDetailDTO>
    {
        private readonly IEventRepository eventRepository;

        public readonly IMapper mapper;

        public GetEventByIdQuery(IEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository 
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper 
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public EventDetailDTO Execute(GetEventByIdQueryParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var eventModel = eventRepository
                .Get(parameters.Id);

            if (eventModel == null)
                throw new ResourceNotFoundException();

            var result = mapper.Map<EventDetailDTO>(eventModel);

            return result;
        }
    }
}
