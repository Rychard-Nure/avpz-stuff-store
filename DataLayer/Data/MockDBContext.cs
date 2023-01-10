using DataLayer.Entities;

namespace DataLayer.Data
{
    public class MockDBContext : IDisposable
    {
        public MockDBContext()
        {
            Categories = new List<GameCategory>()
            {
                new GameCategory() { Name = "Advanture"},
                new GameCategory() { Name = "Races"},
                new GameCategory() { Name = "Strategy"},
                new GameCategory() { Name = "Rpg"},
                new GameCategory() { Name = "Action"},
                new GameCategory() { Name = "Puzzle & skill"},
                new GameCategory() { Name = "Other"}
            };

            Games = new List<Game>()
            {
                new Game()
                {
                    Name = "Need For Speed",
                    Price = 99,
                    Description = "The series generally centers around illicit street racing and tasks players to complete various types of races while evading the local law enforcement in police pursuits.",
                    ImagePath = "images/race.jpg",
                    IsDeleted = false,
                    Sold = 9,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = Categories.First()},
                        new CategoryGame() {Category = Categories.Last()}
                    }
                },
                new Game()
                {
                    Name = "World War III",
                    Price = 150.0m,
                    Description = "Gameplay player versus player shooter game in which up to one hundred players fight in a battle royale, a type of large-scale last man standing deathmatch where players fight to remain the last alive.",
                    ImagePath = "images/big.jpg",
                    IsDeleted = false,
                    Sold = 13,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = Categories.ToList()[1]},
                        new CategoryGame() {Category = Categories.ToList()[2]}
                    }
                },
                new Game()
                {
                    Name = "PUBG",
                    Price = 5,
                    Description = "Gameplay. PUBG is a player versus player shooter game in which up to one hundred players fight in a battle royale, a type of large-scale last man standing deathmatch where players fight to remain the last alive.",
                    ImagePath = "images/pubg.jpg",
                    IsDeleted = false,
                    Sold = 1212,
                    Categories = new List<CategoryGame>()
                    {
                        new CategoryGame() {Category = Categories.First()},
                        new CategoryGame() {Category = Categories.Last()}
                    }
                }
            };
        }

        public IEnumerable<GameCategory> Categories { get; set; }
        public IEnumerable<Game> Games { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
