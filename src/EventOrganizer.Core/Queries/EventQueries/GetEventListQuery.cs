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

            // TO DO: optimize search method

            var events = eventRepository
                .GetAll();

            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                events = events.Where(x => x.Title.Contains(parameters.Filter, StringComparison.OrdinalIgnoreCase)
                || x.Description.Contains(parameters.Filter, StringComparison.OrdinalIgnoreCase));
            }

            if (parameters.Tags != null && parameters.Tags.Any())
            {
                events = events.Where(x =>
                {
                    foreach (var tag in parameters.Tags)
                    {
                        if (!x.EventTags.Any(y => y.Keyword == tag))
                            return false;
                    }
                    return true;
                });
            }
                
            var result = events
                .Skip(parameters.Skip)
                .Take(parameters.Top)
                .ToArray()
                .Select(e => mapper.Map<EventDTO>(e))
                .ToList();

            return result;
        }
    }
}
