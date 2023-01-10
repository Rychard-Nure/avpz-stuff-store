using BLL.Models;

namespace BLL.Interfaces
{
    public interface IGameCategoryService
    {
        Task<IEnumerable<GameCategoryModel>> GetAllGameCategoriesAsync();
    }
}
