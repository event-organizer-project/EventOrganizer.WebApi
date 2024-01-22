using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Queries.CalendarQueries
{
    public class GetWeeklyScheduleQuery : IQuery<GetWeeklyScheduleQueryParameters, WeeklyScheduleDTO>
    {
        private readonly IEventRepository eventRepository;

        public readonly IMapper mapper;

        private readonly IUserHandler userHandler;

        private readonly IWeekHandler weekHandler;

        public GetWeeklyScheduleQuery(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler, IWeekHandler weekHandler)
        {
            this.eventRepository = eventRepository
                ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
            this.weekHandler = weekHandler
                ?? throw new ArgumentNullException(nameof(weekHandler));
        }

        public WeeklyScheduleDTO Execute(GetWeeklyScheduleQueryParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var user = userHandler.GetCurrentUser();

            var userOwnEvents = eventRepository
                .GetAll()
                .Where(e => e.Members.Select(x => x.Id).Contains(user.Id))
                //TO DO: add where for start and end time dependigs to weeks
                .ToArray();

            var week = weekHandler.GetWeek(parameters.WeekOffset, parameters.TimeZoneOffsetInMinutes);

            foreach( var day in week.WeekDays)
            {
                day.Events = userOwnEvents
                    .Where(e => e.StartDate.Date == day.Date.UtcDateTime.Date)
                    .Select(e => mapper.Map<EventDTO>(e))
                    .ToArray();
            }

            return week;
        }
    }
}
