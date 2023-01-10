using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public class GameRepository : Repository<Game>, IGameInterface
    {
        private readonly GameStoreDBContext dbContext;

        public GameRepository(GameStoreDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetAllGamesWithCategories()
            => await dbContext.Games.Include(i => i.Categories)
                                    .ThenInclude(i => i.Category)
                                    .ToListAsync();

        public async Task<Game> GetByIdWithCategories(int id)
            => (await GetAllGamesWithCategories()).FirstOrDefault(i => i.Id == id);

        public Task RemoveMMs(int id)
        {
            var ss = dbContext.CategoryGames.Where(i => i.GameId == id).ToList();
            for (int i = 0; i < ss.Count; i++)
            {
                dbContext.CategoryGames.Remove(ss[i]);
                dbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public async Task<Game> UpdateGameAsync(Game game)
        {
            var listMM = dbContext.CategoryGames.ToList();
            var listG = game.Categories;
            var ss = listMM.Where(i => i.GameId == game.Id).ToList();
            for (int i = 0; i < ss.Count; i++)
            {
                dbContext.CategoryGames.Remove(ss[i]);
                dbContext.SaveChanges();
            }

            for (int i = 0; i < listG.Count; i++)
            {
                dbContext.CategoryGames.Add(listG[i]);
                dbContext.SaveChanges();
            }
            
            dbContext.Games?.Update(game);

            return await GetByIdWithCategories(game.Id);
        }
    }
}
