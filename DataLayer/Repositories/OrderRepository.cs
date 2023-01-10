using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderInterface
    {
        public OrderRepository(GameStoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
