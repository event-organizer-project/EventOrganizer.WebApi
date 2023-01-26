using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;

namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetEventListQuery : IQuery<GetEventListQueryParameters, IList<EventDTO>>
    {
        private readonly IEventRepository eventRepository;

        public readonly IMapper mapper;

        public GetEventListQuery(IEventRepository eventRepository, IMapper mapper)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IList<EventDTO> Execute(GetEventListQueryParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var events = eventRepository
                .GetAll()
                //TO DO: add search by EventTags and title filter
                .Skip(parameters.Skip)
                .Take(parameters.Top)
                .Select(e => mapper.Map<EventDTO>(e))
                .ToList();

            return events;
        }
    }
}
