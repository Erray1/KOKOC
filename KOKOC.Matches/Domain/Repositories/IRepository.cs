using Ardalis.Result;
using KOKOC.Matches.Domain.Entities;

namespace KOKOC.Matches.Domain.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public Task<Result<TEntity>> GetAsync(TKey id);
        public Task<Result<List<TEntity>>> GetAllAsync();
        public Task<Result> DeleteAsync(TKey id);
        public Task<Result> EditAsync(TKey id, TEntity source);
        public Task<Result<TEntity>> AddAsync(TEntity entity);
    }
}
