using BLL.Services;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Moq;

namespace GameStore.Test.BLL
{
    internal class GameCatgoryServiceTest
    {
        [Test]
        public async Task GameCategoryService_GetAllGameCategoriesAsync_ReturnAllGameComments()
        {
            var expected = UnitTestHelper.comments;
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork
                .Setup(m => m.GameCategories.GetAllAsync())
                .ReturnsAsync(GameCategories.AsQueryable());

            var categoryService = new GameCategoryService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());

            var actual = await categoryService.GetAllGameCategoriesAsync();
        }

        #region TestData
        public List<GameCategory> GameCategories =>
            new List<GameCategory>
            {
                new GameCategory() { Id = 1, Name = "test1"},
                new GameCategory() { Id = 2, Name = "test2"},
            };
        #endregion
    }
}
