using EventOrganizer.Core.Repositories;
using EventOrganizer.Domain.Models;

namespace EventOrganizer.EF.MySql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventOrganazerDbContext dbContext;

        public UserRepository(EventOrganazerDbContext eventOrganazerDbContext)
        {
            dbContext = eventOrganazerDbContext
                ?? throw new ArgumentNullException(nameof(eventOrganazerDbContext));
        }

        IEnumerable<User> IUserRepository.GetAll()
        {
            return dbContext.Users;
        }

        public User? GetUserById(int id)
        {
            var model = dbContext.Users.FirstOrDefault(x => x.Id == id);

            return model;
        }
    }
}
