using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using FluentValidation;

namespace EventOrganizer.Core.Validators
{
    public class EventValidator : AbstractValidator<EventModel>
    {
        private readonly IEventRepository eventRepository;
        private readonly IUserHandler userHandler;

        public EventValidator(IEventRepository eventRepository, IUserHandler userHandler)
        {
            this.eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            this.userHandler = userHandler ?? throw new ArgumentNullException(nameof(userHandler));

            RuleFor(x => x.Title).Length(5, 50);
            RuleFor(x => x.Description).Length(10, 1000);
            RuleFor(x => x).Must(CheckEventOverlap).WithMessage("The event has conflicts with other events.");
        }

        private bool CheckEventOverlap(EventModel eventModel)
        {
            var currentUserId = userHandler.GetCurrentUser().Id;

            return !eventRepository.GetAll()
                .Where(x => x.Members.Any(u => u.Id == currentUserId) && x.StartDate == eventModel.StartDate)
                .Any(x => (x.EndTime > eventModel.StartTime && x.EndTime < eventModel.EndTime)
                    || (x.StartTime < eventModel.EndTime && x.StartTime > eventModel.StartTime)
                    || (x.StartTime < eventModel.StartTime && x.EndTime > eventModel.EndTime));
        }
    }
}
