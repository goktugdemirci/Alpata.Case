using Alpata.Case.Domain.Base;

namespace Alpata.Case.Domain.Repository
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveChangesAsync();
        IQueryable<T> GetQueryable();
    }
}
