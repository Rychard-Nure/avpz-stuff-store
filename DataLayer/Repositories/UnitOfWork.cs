using DataLayer.Data;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDBContext _dbcontext;

        public UnitOfWork(GameStoreDBContext dbcontext,
                           IGameCategoryInterface gameCategoryInterface,
                           IGameInterface gameInterface,
                           ICommentInterface comments,
                           IOrderInterface orderInterface,
                           IOrderDetailInterface orderDetail)
        {
            _dbcontext = dbcontext;
            GameCategories = gameCategoryInterface;
            Games = gameInterface;
            Comments = comments;
            Orders = orderInterface;
            OrderDetails = orderDetail;

        }
        public IGameCategoryInterface GameCategories { get; }

        public IGameInterface Games { get; }

        public ICommentInterface Comments { get; }

        public IOrderInterface Orders { get; }

        public IOrderDetailInterface OrderDetails { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }
    }
}
