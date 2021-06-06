using Core.Entities;
using Core.Interfaces;
using Core.Services;
using Core.Specifications;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> FetchAsync(string login)
        {
            return await _repository.FirstOrDefaultAsync(new FetchUserByUsernameOrEmailSpecification(login));
        }

        public async Task<User> RegisterAsync(User entity)
        {
            return await _repository.AddAsync(entity);
        }
    }
}
