using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Queries.EventQueries
{
    public class GetCurrentUserOwnEventsQuery : IQuery<VoidParameters, UserOwnEventsDTO>
    {
        private readonly IEventRepository eventRepository;

        public readonly IMapper mapper;

        private readonly IUserHandler userHandler;

        public GetCurrentUserOwnEventsQuery(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
        }

        public UserOwnEventsDTO Execute(VoidParameters parameters)
        {
            var user = userHandler.GetCurrentUser();

            var userOwnEvents = eventRepository
                .GetAll()
                .Where(e => e.Owner.Id == user.Id || e.Members.Select(x => x.Id).Contains(user.Id))
                .ToArray();

            var result = new UserOwnEventsDTO
            {
                CreatedEvents = userOwnEvents
                    .Where(e => e.Owner.Id == user.Id)
                    .Select(e => mapper.Map<EventDTO>(e))
                    .ToList(),
                JoinedEvents = userOwnEvents
                    .Where(e => e.Members.Select(x => x.Id).Contains(user.Id))
                    .Select(e => mapper.Map<EventDTO>(e))
                    .ToList()
            };

            return result;
        }
    }
}
