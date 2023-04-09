using EventOrganizer.Core.CustomExceptions;
using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Services
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserContextAccessor userContextAccessor;

        private readonly IUserRepository userRepository;

        public UserHandler(IUserContextAccessor userContextAccessor,
            IUserRepository userRepository)
        {
            this.userContextAccessor = userContextAccessor
                ?? throw new ArgumentNullException(nameof(userContextAccessor));
            this.userRepository = userRepository
                ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public User GetCurrentUser()
        {
            var userId = userContextAccessor.GetUserId();

            var user = userRepository.GetUserById(userId) 
                ?? throw new UserHandlingException();

            return user;
        }
    }
}
