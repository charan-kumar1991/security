using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class where TKey : struct
    {
        private readonly UserContext _dbContext;
        public BaseRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> specification)
        {
            IQueryable<T> queryable = ApplySpecification(specification);
            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
        }
    }
}
