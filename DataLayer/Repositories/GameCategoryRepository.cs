using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace DataLayer.Repositories
{
    public class GameCategoryRepository : Repository<GameCategory>, IGameCategoryInterface
    {
        public GameCategoryRepository(GameStoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
