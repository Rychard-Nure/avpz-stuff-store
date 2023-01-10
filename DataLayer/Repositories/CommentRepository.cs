using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentInterface
    {
        public CommentRepository(GameStoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
