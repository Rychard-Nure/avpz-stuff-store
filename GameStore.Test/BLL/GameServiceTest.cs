using BLL.Models;
using BLL.Services;
using DataLayer.Entities;
using DataLayer.Interfaces;
using FluentAssertions;
using Moq;

namespace GameStore.Test.BLL
{
    internal class GameServiceTest
    {
        [Test]
        public async Task GameService_GetAllGamesAsync_ReturnsAllGames()
        {
            //arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Games.GetAllAsync())
                .ReturnsAsync(Games.AsEnumerable());

            var commentService = new CommentService(mockUnitOfWork.Object);
            var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), commentService);
                
            //actual
            var games = await gameService.GetAllGamesAsync();

            var expected = GameModels;

            //assert
            games.Should().BeEquivalentTo(expected);  
        }

        [Test]
        public async Task GameService_AddGameAsync()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Games.AddAsync(It.IsAny<Game>()))
                    .ReturnsAsync(Games.First());
            mockUnitOfWork.Setup(m => m.GameCategories.GetAllAsync());

            var commentService = new CommentService(mockUnitOfWork.Object);
            var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile(), commentService);
            var game = GameModels.First();

            //actual
            var model = await gameService.AddGameAsync(game.Name, game.Description, game.Price, game.ImagePath, game.Categories.Select(i => i.Name).ToList());

            //assert
            model.Should().BeEquivalentTo(game);
        }

        #region TestData
        public List<GameModel> GameModels =>
            new List<GameModel>
            {
                    new GameModel()
                    {
                        Id = 1,
                        Name = "game1",
                        Description= "Description1",
                        Sold = 0,
                        IsDeleted= false,
                        ImagePath = "path1",
                        Price = 100
                    },
                    new GameModel()
                    {
                        Id = 2,
                        Name = "game2",
                        Description= "Description2",
                        Sold = 0,
                        IsDeleted= false,
                        ImagePath = "path2",
                        Price = 200
                    }
            };
        public List<Game> Games =>
            new List<Game>
            {
                    new Game()
                    {
                        Id = 1,
                        Name = "game1",
                        Description= "Description1",
                        Sold = 0,
                        IsDeleted= false,
                        ImagePath = "path1",
                        Price = 100
                    },
                    new Game()
                    {
                        Id = 2,
                        Name = "game2",
                        Description= "Description2",
                        Sold = 0,
                        IsDeleted= false,
                        ImagePath = "path2",
                        Price = 200
                    }
            };
        #endregion
    }
}
