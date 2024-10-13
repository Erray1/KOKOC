using Ardalis.Result;
using Erray.ServicesScanning;
using KOKOC.Matches.Domain;
using KOKOC.Matches.Domain.Repositories;
using KOKOC.Matches.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KOKOC.Matches.Application
{
    [SuppressAutomaticRegistration]
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        private readonly MatchesDbContext _dbContext;
        private readonly EqualityComparer<TKey> _comparer = EqualityComparer<TKey>.Default;
        public GenericRepository(MatchesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<TEntity>> AddAsync(TEntity entity)
        {
            var entry = _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Result> DeleteAsync(TKey id)
        {
            var entity = await _dbContext
                .Set<TEntity>()
                .SingleOrDefaultAsync(x => _comparer.Equals(x.Id, id));
            if (entity is null)
            {
                return Result.NotFound();
            }
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> EditAsync(TKey id, TEntity source)
        {
            var entity = await _dbContext
                .Set<TEntity>()
                .SingleOrDefaultAsync(x => _comparer.Equals(x.Id, id));
            if (entity is null)
            {
                return Result.NotFound();
            }
            new EntitiesEditor<TEntity>().Edit(entity, source);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<List<TEntity>>> GetAllAsync()
        {
            var entities = await _dbContext
                .Set<TEntity>()
                .ToListAsync();
            return entities is null || entities.Count == 0 ? Result.NotFound() : entities;
        }

        public async Task<Result<TEntity>> GetAsync(TKey id)
        {
            var entity = await _dbContext
                .Set<TEntity>()
                .SingleOrDefaultAsync(x => _comparer.Equals(x.Id, id));
            return entity is null ? Result.NotFound() : entity;
        }
    }
}
