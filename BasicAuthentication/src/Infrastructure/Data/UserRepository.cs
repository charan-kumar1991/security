using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UserRepository: BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(UserContext dbContext): base(dbContext)
        {

        }
    }
}
