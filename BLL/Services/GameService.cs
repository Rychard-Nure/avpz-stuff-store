using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public GameService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           ICommentService commentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _commentService = commentService;
        }
        public async Task<IEnumerable<GameModel>> GetAllGamesByNameAsync(string? name, SortType sort)
        {
            var Games = await _unitOfWork.Games.GetAllGamesWithCategories();

            switch (sort)
            {
                case SortType.Unselect:
                case SortType.New: Games = Games.OrderByDescending(i => i.Id); break;
                case SortType.Free: Games = Games.Where(i => i.Price == 0); break;
                case SortType.Popular: Games = Games.OrderByDescending(i => i.Sold); break;
            }
            
            var gameModels = Games.Select(i => _mapper.Map<GameModel>(i));

            if (!(string.IsNullOrEmpty(name)))
                return gameModels.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();

            if (Games.Count() == 0 && sort != SortType.Free)
            {
                AddMockData().Wait();
            }

            return gameModels;
        }

        public async Task AddMockData()
        {
            var dbContext = new MockDBContext();

            foreach (var category in dbContext.Categories)
            {
                await _unitOfWork.GameCategories.AddAsync(category);
                await _unitOfWork.SaveAsync();
            }

            foreach (var game in dbContext.Games)
            {
                await _unitOfWork.Games.AddAsync(game);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
            => _mapper.Map<GameModel>(await _unitOfWork.Games.GetByIdWithCategories(id));

        public async Task<GameModel> AddGameAsync(string name, string description, decimal price, string imagePath, List<string> categories)
        {
            var categoryList = await _unitOfWork.GameCategories.GetAllAsync();
            var newGame = new GameModel()
            {
                Name = name,
                Description = description,
                Price = price,
                ImagePath = imagePath,
                Categories = categories.Select(c => _mapper.Map<GameCategoryModel>(categoryList.FirstOrDefault(i => i.Name == c))).ToList()
            };

            var game = _mapper.Map<Game>(newGame);
            var model = await _unitOfWork.Games.AddAsync(game);
            await _unitOfWork.SaveAsync();
            newGame.Id = model.Id;

            return newGame;
        }

        public async Task<bool> Exist(string name)
            => (await _unitOfWork.Games.GetAllAsync()).Any(i => i.Name == name);

        public async Task<IEnumerable<GameModel>> GetGamesByCategoryId(int categoryId)
            => (await _unitOfWork.Games.GetAllGamesWithCategories())
                                       .Where(g => g.Categories.Any(i => i.CategoryId == categoryId))
                                       .Select(i => _mapper.Map<GameModel>(i));

        public async Task<GameModel> UpdateGame(GameModel gameModel)
        {
            var result = await _unitOfWork.Games.UpdateGameAsync(_mapper.Map<Game>(gameModel));
            await _unitOfWork.SaveAsync();
            return _mapper.Map<GameModel>(result);
        }

        public async Task<IEnumerable<GameModel>> GetAllGamesAsync()
            => (await _unitOfWork.Games.GetAllAsync())
                                 .Select(g => _mapper.Map<GameModel>(g));

        public async Task Delete(GameModel game, IEnumerable<UserModel>? users)
        {
            var gameComments = await _commentService.GetGameComments(game.Id, users);
            foreach (var comment in gameComments)
            {
                await _commentService.DeleteGeneralAsync(comment.Id);
            }
            await _unitOfWork.Games.RemoveMMs(game.Id);
            await _unitOfWork.SaveAsync();  
            await _unitOfWork.Games.DeleteByIdAsync(game.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}
