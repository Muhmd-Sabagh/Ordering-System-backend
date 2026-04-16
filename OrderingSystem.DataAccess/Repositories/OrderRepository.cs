using OrderingSystem.DataAccess.Data;
using OrderingSystem.Global.Entities;
using OrderingSystem.Global.Interfaces.Repositories;

namespace OrderingSystem.DataAccess.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}
