using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Services
{
    public interface IUserHandler
    {
        User GetCurrentUser();
    }
}
