using AutoMapper;
using EventOrganizer.Core.DTO;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMapUserToUserDTO();
        }

        public void CreateMapUserToUserDTO()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
