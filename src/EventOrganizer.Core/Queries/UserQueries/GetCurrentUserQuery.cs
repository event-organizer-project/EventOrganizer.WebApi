using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Core.Services;

namespace EventOrganizer.Core.Queries.UserQueries
{
    public class GetCurrentUserQuery : IQuery<GetCurrentUserQueryParameters, UserDTO>
    {
        private readonly IUserHandler userHandler;

        public readonly IMapper mapper;

        public GetCurrentUserQuery(IUserHandler userHandler, IMapper mapper)
        {
            this.userHandler = userHandler
                ?? throw new ArgumentNullException(nameof(userHandler));
            this.mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public UserDTO Execute(GetCurrentUserQueryParameters parameters)
        {
            var user = userHandler.GetCurrentUser();

            var result = mapper.Map<UserDTO>(user);

            return result;
        }
    }
}
