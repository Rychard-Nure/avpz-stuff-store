namespace BLL.Models
{
    public class GameCategoryModel
    {
        public GameCategoryModel()
        {
            CategoryGamesIds = new List<int>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<int> CategoryGamesIds { get; set; }
    }
}
