using EventOrganizer.Domain.Models;

namespace EventOrganizer.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User? GetUserById(int id);
    }
}
