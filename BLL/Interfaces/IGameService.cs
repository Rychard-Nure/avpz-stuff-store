using BLL.Models;

namespace BLL.Interfaces
{
    public interface IGameService
    {
        Task AddMockData();
        Task<IEnumerable<GameModel>> GetAllGamesAsync();
        Task<IEnumerable<GameModel>> GetAllGamesByNameAsync(string? name, SortType sort);
        Task<IEnumerable<GameModel>> GetGamesByCategoryId(int categoryId);
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> AddGameAsync(string name, string description, decimal price, string imagePath, List<string> categories);
        Task<GameModel> UpdateGame(GameModel gameModel);
        Task Delete(GameModel game, IEnumerable<UserModel>? users);
        Task<bool> Exist(string name);
    }
}
