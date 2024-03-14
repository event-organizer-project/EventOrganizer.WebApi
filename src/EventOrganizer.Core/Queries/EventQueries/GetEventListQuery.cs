using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetEventListQuery : IQuery<GetEventListQueryParameters, IList<EventDTO>>
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHandler userHandler;
        private readonly IMapper mapper;

        public GetEventListQuery(IEventRepository eventRepository, IMapper mapper, IUserHandler userHandler)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
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

            if (parameters.OnlyForCurrentUser)
            {
                var user = userHandler.GetCurrentUser();
                events = events.Where(x => x.Members.Any(x => x.Id == user.Id));
            }

            if (parameters.StartingFrom.HasValue)
            {
                events = events.Where(x => x.StartDate > parameters.StartingFrom);
            }

            if (parameters.EndingBefore.HasValue)
            {
                events = events.Where(x => x.EndDate < parameters.EndingBefore);
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
