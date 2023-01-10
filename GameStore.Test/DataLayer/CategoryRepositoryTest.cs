using DataLayer.Data;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Test.DataLayer
{
    internal class CategoryRepositoryTest
    {
        [Test]
        public async Task GameCategoryRepository_GetAllAsync_ReturnAllValues()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            var gameCategorys = await gameCategoryRepository.GetAllAsync();

            Assert.That(gameCategorys, Is.EqualTo(UnitTestHelper.categories).Using(new GameCategoryEqualityComparer()));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GameCategoryRepository_GetByIdAsync_ReturnSingleGameCategory(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            var gameCategory = await gameCategoryRepository.GetByIdAsync(id);
            var expectedgameCategory = UnitTestHelper.categories.FirstOrDefault(i => i.Id == id);

            Assert.That(gameCategory, Is.EqualTo(expectedgameCategory).Using(new GameCategoryEqualityComparer()));
        }

        [Test]
        public async Task GameCategoryRepository_AddAsync_AddGameCategoryToDatabase()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            var gameCategory = await gameCategoryRepository.GetByIdAsync(1);
            gameCategory.Id = 8;
            await gameCategoryRepository.AddAsync(gameCategory);
            await context.SaveChangesAsync();

            Assert.That(context.GameCategories.Count(), Is.EqualTo(8));
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GameCategoryRepository_DeleteByIdAsync_DeleteGameCategory(int id)
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            await gameCategoryRepository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            Assert.That(context.GameCategories.Count(), Is.EqualTo(6));
        }

        [Test]
        public async Task GameCategoryRepository_DeleteAsync_DeleteGameCategory()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            var gameCategory = await gameCategoryRepository.GetByIdAsync(1);
            gameCategoryRepository.Delete(gameCategory);
            await context.SaveChangesAsync();

            Assert.That(context.GameCategories.Count(), Is.EqualTo(6));
        }

        [Test]
        public async Task GgameCategoryRepository_Update_UpdategameCategory()
        {
            using var context = new GameStoreDBContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameCategoryRepository = new GameCategoryRepository(context);
            var gameCategory = await gameCategoryRepository.GetByIdAsync(1);
            gameCategory.Name = "Updated";
            var result = gameCategoryRepository.Update(gameCategory);
            await context.SaveChangesAsync();

            Assert.That(result, Is.EqualTo(gameCategory));
        }
    }
}
