using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailInterface
    {
        public OrderDetailRepository(GameStoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
