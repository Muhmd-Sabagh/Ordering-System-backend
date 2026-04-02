using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Repositories;

namespace OrderingSystem.Global.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Expose repositories for your entities
        IGenericRepository<Customer> Customers { get; }
        IGenericRepository<Order> Orders { get; }

        // Commits all changes in a single transaction
        Task<int> CompleteAsync();
    }
}
