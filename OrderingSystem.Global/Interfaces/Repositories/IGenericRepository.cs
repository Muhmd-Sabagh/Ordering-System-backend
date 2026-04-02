using System.Linq.Expressions;

namespace OrderingSystem.Global.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        // Allows custom filtering (e.g., getting orders for a specific customer)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // CRITICAL for our business rule: Allows bypassing the soft-delete filter
        Task<IEnumerable<T>> FindWithDeletedAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
