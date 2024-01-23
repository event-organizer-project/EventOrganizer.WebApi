using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Helpers;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using FluentValidation;

namespace EventOrganizer.Core.Commands.EventCommands
{
    public class CreateEventCommand : ICommand<CreateEventCommandParameters, EventDetailDTO>
    {
        private readonly IEventRepository eventRepository;

        private readonly IMapper mapper;

        private readonly IUserHandler userHandler;

        private readonly ISchedulerClient schedulerClient;

        private readonly IValidator<EventModel> eventValidator;

        public CreateEventCommand(IEventRepository eventRepository, IMapper mapper,
            IUserHandler userHandler, ISchedulerClient schedulerClient, IValidator<EventModel> eventValidator)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userHandler = userHandler ?? throw new ArgumentNullException(nameof(userHandler));
            this.schedulerClient = schedulerClient ?? throw new ArgumentNullException(nameof(schedulerClient));
            this.eventValidator = eventValidator ?? throw new ArgumentNullException(nameof(eventValidator));
        }

        public EventDetailDTO Execute(CreateEventCommandParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var eventModel = mapper.Map<EventModel>(parameters.EventDetailDTO);

            eventModel.Owner = userHandler.GetCurrentUser();
            eventModel.Members = new[] { eventModel.Owner };

            eventValidator.ValidateAndThrow(eventModel);

            var result = eventRepository.Create(eventModel);

            if (EventSchedulingHelper.IsDateTheRestOfToday(result.StartDate))
            {
                schedulerClient.AddEventToSchedule(result.Id, result.OwnerId);
            }

            return mapper.Map<EventDetailDTO>(result);
        }
    }
}
