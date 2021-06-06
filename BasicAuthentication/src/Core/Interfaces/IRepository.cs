using Ardalis.Specification;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<T, TKey> where T : class where TKey : struct
    {
        Task<T> GetByIdAsync(TKey id);
        Task<T> FirstOrDefaultAsync(ISpecification<T> specification);
        Task<T> AddAsync(T entity);
    }
}
