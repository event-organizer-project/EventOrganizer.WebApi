using EventOrganizer.Core.Validators;
using EventOrganizer.Domain.Models;
using FluentValidation;

namespace EventOrganizer.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<EventModel>, EventValidator>();
        }
    }
}
