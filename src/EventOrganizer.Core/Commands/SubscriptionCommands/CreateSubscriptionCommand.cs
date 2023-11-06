using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Core.Services;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Commands.SubscriptionCommands
{
    public class CreateSubscriptionCommand : ICommand<CreateSubscriptionCommandParameters, VoidResult>
    {
        private readonly ISubscriptionRepository subscriptionRepository;

        private readonly IUserHandler userHandler;

        public CreateSubscriptionCommand(ISubscriptionRepository subscriptionRepository, IUserHandler userHandler)
        {
            this.subscriptionRepository = subscriptionRepository
                ?? throw new ArgumentNullException(nameof(subscriptionRepository));
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
        }

        public VoidResult Execute(CreateSubscriptionCommandParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            var currentUser = userHandler.GetCurrentUser();

            var subscription = new Subscription
            {
                Endpoint = parameters.Endpoint,
                P256DH = parameters.P256DH,
                Auth = parameters.Auth,
                UserId = currentUser.Id
            };

            subscriptionRepository.Create(subscription);

            return new VoidResult();
        }
    }
}
