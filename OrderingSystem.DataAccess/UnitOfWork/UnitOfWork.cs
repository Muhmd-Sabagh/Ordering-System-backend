using OrderingSystem.DataAccess.Data;
using OrderingSystem.DataAccess.Repositories;
using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Repositories;
using OrderingSystem.Global.Interfaces.UnitOfWork;

namespace OrderingSystem.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IGenericRepository<Customer>? _customers;
        private IGenericRepository<Order>? _orders;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);

        public IGenericRepository<Order> Orders => _orders ??= new GenericRepository<Order>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
