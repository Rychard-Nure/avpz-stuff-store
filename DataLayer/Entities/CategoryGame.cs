namespace DataLayer.Entities
{
    public class CategoryGame
    {
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public int CategoryId { get; set; }
        public GameCategory? Category { get; set; }
    }
}
