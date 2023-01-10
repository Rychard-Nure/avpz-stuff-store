using DataLayer.Data;
using DataLayer.Repositories;
namespace GameStore.Test.DataLayer
{
    [TestFixture]
    public class GameRepositoryTest
    {
        [Test]
        public async Task GameRepository_GetAllAsync_ReturnAllValues()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var games = (await gameRepository.GetAllGamesWithCategories()).ToList();
            var expected = UnitTestHelper.games;

            Assert.That(games, Is.EqualTo(expected).Using(new GameEqualityComparer()));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GameRepository_GetByIdAsync_ReturnSingleGame(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var game = await gameRepository.GetByIdWithCategories(id);
            var expectedGame = UnitTestHelper.games.FirstOrDefault(i => i.Id == id);

            Assert.That(game, Is.EqualTo(expectedGame).Using(new GameEqualityComparer()));
        }

        [Test]
        public async Task GameRepository_AddAsync_AddGameToDatabase()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var game = await gameRepository.GetByIdAsync(1);
            game.Id = 4;
            await gameRepository.AddAsync(game);
            await context.SaveChangesAsync();

            Assert.That(context.Games.Count(), Is.EqualTo(4));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GameRepository_DeleteByIdAsync_DeleteGame(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            await gameRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            Assert.That(context.Games.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GameRepository_DeleteAsync_DeleteGame()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var game = await gameRepository.GetByIdAsync(1);
            gameRepository.Delete(game);
            await context.SaveChangesAsync();

            Assert.That(context.Games.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GameRepository_Update_UpdateGame()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var game = await gameRepository.GetByIdAsync(1);
            game.Name = "Updated";
            var result = gameRepository.Update(game);
            await context.SaveChangesAsync();

            Assert.That(result, Is.EqualTo(game));
        }
    }
}
