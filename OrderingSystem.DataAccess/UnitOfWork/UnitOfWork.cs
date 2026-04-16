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

        private ICustomerRepository? _customers;
        private IOrderRepository? _orders;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customers ??= new CustomerRepository(_context);

        public IOrderRepository Orders => _orders ??= new OrderRepository(_context);

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
