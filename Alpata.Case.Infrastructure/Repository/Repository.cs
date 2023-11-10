using Alpata.Case.Domain.Base;
using Alpata.Case.Domain.Repository;
using Alpata.Case.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Alpata.Case.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AlpataCaseContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AlpataCaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(Guid id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id==id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Add(entity);
            await SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity =await GetByIdAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
