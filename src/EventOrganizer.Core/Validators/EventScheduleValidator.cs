using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;
using FluentValidation;

namespace EventOrganizer.Core.Validators
{
    public class EventScheduleValidator : AbstractValidator<EventModel>
    {
        public EventScheduleValidator(IEventRepository eventRepository, IUserHandler userHandler)
        {
            if (eventRepository == null) throw new ArgumentNullException(nameof(eventRepository));
            if (userHandler == null) throw new ArgumentNullException(nameof(userHandler));

            var currentUserId = userHandler.GetCurrentUser().Id;

            RuleFor(x => x).Must(eventModel =>
            {
                var currentUserId = userHandler.GetCurrentUser().Id;
                return true;
                /*
                return !eventRepository.GetAll()
                    .Where(x => x.Members.Any(u => u.Id == currentUserId) && x.StartDate == eventModel.StartDate)
                    .Any(x => (x.EndDate > eventModel.StartDate && x.EndTime < eventModel.EndTime)
                        || (x.StartTime < eventModel.EndTime && x.StartTime > eventModel.StartTime)
                        || (x.StartTime < eventModel.StartTime && x.EndTime > eventModel.EndTime));*/
            }).WithMessage("The event has conflicts with other events.");
        }
    }
}