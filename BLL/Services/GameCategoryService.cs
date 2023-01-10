using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DataLayer.Entities;
using DataLayer.Interfaces;

namespace BLL.Services
{
    public class GameCategoryService : IGameCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameCategoryService(IUnitOfWork unitOfWork,
                                   IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameCategoryModel>> GetAllGameCategoriesAsync()
            => (await _unitOfWork.GameCategories.GetAllAsync())
                                                .Select(i => _mapper.Map<GameCategoryModel>(i));
    }
}
