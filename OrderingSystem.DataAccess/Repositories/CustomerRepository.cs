using OrderingSystem.DataAccess.Data;
using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Repositories;

namespace OrderingSystem.DataAccess.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }
    }
}
