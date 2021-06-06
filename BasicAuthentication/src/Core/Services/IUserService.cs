using Core.Entities;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User entity);
        Task<User> FetchAsync(string login);
    }
}
